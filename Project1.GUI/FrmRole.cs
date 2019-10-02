using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Project1.Core;
using Project1.Domain;
using DevExpress.XtraGrid.Views.Grid;

namespace Project1.GUI
{
    public partial class FrmRole : FrmBase
    {
        Core_Role _core = new Core_Role();
        string _id;
        public FrmRole()
        {
            InitializeComponent();
        }

        public override void DisplayInfo()
        {
            _dtb = _core.GetAll_Role();
            if (_dtb != null)
            {
                grvRole.DataSource = _dtb;
            }
        }

        public override void ResetText()
        {
            txtName.Text = null;
            txtDescr.Text = null;
        }

        public override void ChangeStatus(bool editTable = true)
        {
            txtName.ReadOnly = editTable;
            txtDescr.ReadOnly = editTable;
        }

        public override void ChangeNameButton(bool isAdd = true)
        {
            if (isAdd)
            {
                btnEdit.Text = "Lưu lại";
                btnEdit.ImageOptions.Image = global::Project1.GUI.Properties.Resources.Save;
                btnDelete.Text = "Hủy bỏ";
                btnDelete.ImageOptions.Image = global::Project1.GUI.Properties.Resources.Cancel;
                btnAdd.Enabled = false;
            }
            else
            {
                btnEdit.Text = "Cập nhật";
                btnDelete.Text = "Xóa";
                btnAdd.Enabled = true;
            }
        }

        public override void ClearDataBindings()
        {
            txtName.DataBindings.Clear();
            txtDescr.DataBindings.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdd = true;
            _isSave = true;
            PerformAdd();
            grvRole.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_isSave)
            {
                PerformSave();
            }
            else
            {
                _isAdd = false;
                _isSave = true;
                PerformEdit();
                grvRole.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_isSave)
            {
                PerformCancel();
            }
            else
            {
                PerformDelete();
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            _isAdd = true;
            _isSave = true;
            PerformAdd();
            grvRole.Enabled = false;
        }



        public override bool PerformAdd()
        {
            ClearDataBindings();
            ResetText();
            ChangeStatus(false);
            txtName.Focus();
            ChangeNameButton();
            return true;
        }

        public override bool PerformEdit()
        {
            ChangeStatus(false);
            ChangeNameButton();
            txtName.Focus();
            return true;
        }

        public override bool PerformSave()
        {
            if (!ValidateControl())
            {
                return false;
            }
            bool result = false;
            Role rol = new Role();
            rol.name = txtName.Text;
            rol.descr = txtDescr.Text;

            string msg = "Thêm Vai trò tài khoản";
            if (_isAdd)
            {
                var dt = _core.CheckInfo_Role(rol.name);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dialog = MessageBox.Show(string.Format("Vai trò tài khoản {0} đã tồn tại ngày.\nVui lòng kiểm tra lại ?", rol.name),
                                                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        txtName.Focus();
                        return true;
                    }
                }
                result = _core.Insert_Role(rol);
            }
            else
            {
                rol.id = Int32.Parse(_id);
                result = _core.Update_Role(rol);
                msg = "Cập nhật vai trò tài khoản";
            }
            if (result)
            {
                DisplayInfo();
                if (_isAdd)
                {
                    PerformAdd();
                }
                else
                {
                    _isSave = false;
                    ChangeStatus();
                    ChangeNameButton(false);
                    grvRole.Enabled = true;
                    //dgvMain.Rows[_index].Selected = true;
                }
            }
            else
            {
                MessageBox.Show(msg + " thất bại, Vui lòng xem lại thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isSave = true;
            }
            return true;
        }

        public override bool PerformDelete()
        {
            var dialogResult = MessageBox.Show(string.Format("Đang xóa vai trò tài khoản {0}.\nCó tiếp tục không", txtName.Text), "Thông Báo",
                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Cancel)
            {
                return true;
            }
            var result = _core.Delete_Role(Int32.Parse(_id));
            if (result)
            {
                MessageBox.Show("Xóa vai trò tài khoản hành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayInfo();
            }
            else
            {
                MessageBox.Show("Xóa vai trò tài khoản thất bại.\nVui lòng xem lại công văn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }


        public override bool PerformCancel()
        {
            ClearDataBindings();
            ChangeStatus();
            DisplayInfo();
            ChangeNameButton(false);
            _isSave = false;
            grvRole.Enabled = true;
            return true;
        }



        private bool ValidateControl()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Thông tin vai trò tài khoản không được trống.\nVui lòng nhập vào", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDescr.Text))
            {
                MessageBox.Show("Mô tả vai trò tài khoản không được trống.\nVui lòng nhập vào", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescr.Focus();
                return false;
            }
            return true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!gridView1.IsGroupRow(e.RowHandle)) //Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) //Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; //Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); //Số thứ tự tăng dần
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); //Lấy kích thước của vùng hiển thị Text
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView1); })); //Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); //Nhân -1 để đánh lại số thứ tự tăng dần
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView1); }));
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                txtName.Text = gridView1.GetRowCellValue(e.FocusedRowHandle, "name").ToString();
                txtDescr.Text = gridView1.GetRowCellValue(e.FocusedRowHandle, "descr").ToString();
                _id = gridView1.GetRowCellValue(e.FocusedRowHandle, "id").ToString(); 
            }
        }
        

    }
}