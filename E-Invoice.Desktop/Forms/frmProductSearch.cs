using E_Invoice.Desktop.Controls;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace E_Invoice.Desktop.Forms
{
    public partial class frmProductSearch : A
    {
        public IEnumerable<ProducrVM> products;
        DataTable b;
        
        public frmProductSearch()
        {
            InitializeComponent();
           
        }
      
        private void frmProductSearch_Load(object sender, EventArgs e)
        {
           products = (from p in _unitOfWork.Products.GetAll()
                            select  new ProducrVM
                            {
                                Id = p.Id,
                                Barcode = p.Code,
                                Unit = "قطعة",
                                Name = p.Name,
                                Price = p.SalePrice,
                                Code = p.itemCode
                            }).ToList<ProducrVM>();
            
           // b = products;
            DGVItems.DataSource = products;
            //MappingColumnName(DGVItems);
            toolStripComboBox1.ComboBox.DataSource = PropertiesFromType(new ProducrVM());
            toolStripComboBox1.ComboBox.DisplayMember = "DisplayName";
            toolStripComboBox1.ComboBox.ValueMember = "Name";
        }

        private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataIndexNo = DGVItems.Rows[e.RowIndex].Index.ToString();
            string cellValue = DGVItems.Rows[e.RowIndex].Cells[0].Value.ToString();
            Info._ProductSelected = true;
            Info._SelectedProduct = cellValue.ToInt();
            button1.PerformClick();
        }

        private void advancedDataGridViewSearchToolBar1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var collectionFilterer = new CollectionFilterer();
            var itemsFiltered = collectionFilterer.Filter<ProducrVM>(products, "myItem => myItem."+(string)toolStripComboBox1.ComboBox.SelectedValue+ ".Contains('ثلاجة')").Result.ToList<ProducrVM>();
            DGVItems.DataSource = itemsFiltered;
        }

        public static List<ClassNames> PropertiesFromType(object atype)
        {
            if (atype == null) return new List<ClassNames> { };
            Type t = atype.GetType();
            
            PropertyInfo[] props = t.GetProperties();
            List<ClassNames> propNames = new List<ClassNames>();
            foreach (PropertyInfo prp in props)
            {
                var DisplayName = prp.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .FirstOrDefault() as DisplayNameAttribute;
                propNames.Add(new ClassNames { DisplayName= DisplayName.DisplayName , Name = prp.Name });
            }
            return propNames;
        }

        public void MappingColumnName(DataGridView dgv)
        {
            DataTable dt = dgv.DataSource as DataTable;
            if (dt != null)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = dt.Columns[i].Caption;
                }
            }
        }

        private void DGVItems_SortStringChanged(object sender, EventArgs e)
        {
            
            //b.DataSet.sor DGVItems.SortString;

            //DGVItems.DataSource = b;
        }

        private void DGVItems_FilterStringChanged(object sender, EventArgs e)
        {
            //b.Filter = DGVItems.FilterString;
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            var data = products.Where(x => x.Barcode == txtSearch.Text || x.Name.Contains(txtSearch.Text) || x.Code == txtSearch.Text);
            DGVItems.DataSource = data.ToList();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty)
            {
                var x2 = products.Where(x => x.Name == "item1").ToList();

                products = (from p in _unitOfWork.Products.GetAll()
                            select new ProducrVM
                            {
                                Id = p.Id,
                                Barcode = p.Code,
                                Unit = "قطعة",
                                Name = p.Name,
                                Price = p.SalePrice,
                                Code = p.itemCode
                            }).ToList<ProducrVM>();

            }
            else
            {
                DGVItems.DataSource = products;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DGVItems_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = DGVItems.SelectedRows[0].Index;
            int id = (int)DGVItems.Rows[index].Cells[0].Value.ToString().ToInt();
            Info._SelectedProduct = id;
            button1.PerformClick();
        }
    }
}
    