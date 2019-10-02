using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Project1.GUI
{
    public partial class FrmBase : DevExpress.XtraEditors.XtraForm
    {
        public DataTable _dtb = new DataTable();
        public bool _isSave, _isAdd, _isSearch;
            
        public FrmBase()
        {
            InitializeComponent();
        }

        private void FrmBase_Load(object sender, EventArgs e)
        {
            ClearDataBindings();
            ResetText();
            DisplayInfo();
            ChangeStatus();
            _isSave = false;
        }
        
        #region Virtual
        /// <summary>
        /// Reset text/editvalue tren cac control khi them/sua
        /// </summary>
        public virtual void ResetText() { }
        /// <summary>
        /// Refresh dữ liệu
        /// </summary>
        public virtual void DisplayInfo() { }
        /// <summary>
        /// Clear All DataBindings tren cac control
        /// </summary>
        public virtual void ClearDataBindings() { }
        /// <summary>
        /// Add DataBindings tren cac control
        /// </summary>
        public virtual void DataBindingControl() { }
        /// <summary>
        /// Thực thi tác vụ nhấn nút Add
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformAdd() { return true; }
        /// <summary>
        /// Thực thi tác vụ nhấn nút Edit
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformEdit() { return true; }
        /// <summary>
        /// Thực thi tác vụ nhấn nút Delete
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformDelete() { return true; }
        /// <summary>
        /// Thực thi tác vụ nhấn nút Save
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformSave() { return true; }
        /// <summary>
        /// Perform cancel acion
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformCancel() { return true; }
        /// <summary>
        /// Change status control
        /// </summary>
        public virtual void ChangeStatus(bool isTrue = true) { }
        /// <summary>
        /// Change status button
        /// </summary>
        /// <param name="isAdd"></param>
        public virtual void ChangeNameButton(bool isAdd = true) { }
        #endregion

        public bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

    }
}