using E_Invoice.Domian.Models;
using IronXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Forms
{
    public partial class Upload : A
    {
        public DataTable dtExcel;
        public Upload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// this method will read the excel file and copy its data into a datatable
        /// </summary>
        /// <param name="fileName">name of the file</param>
        /// <returns>DataTable</returns>
        private DataTable ReadExcel(string fileName)
        {
            WorkBook workbook = WorkBook.Load(fileName);
            //// Work with a single WorkSheet.
            ////you can pass static sheet name like Sheet1 to get that sheet
            ////WorkSheet sheet = workbook.GetWorkSheet("Sheet1");
            //You can also use workbook.DefaultWorkSheet to get default in case you want to get first sheet only
            WorkSheet sheet = workbook.DefaultWorkSheet;
            //Convert the worksheet to System.Data.DataTable
            //Boolean parameter sets the first row as column names of your table.
            return sheet.ToDataTable(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file
            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {
                string fileExt = Path.GetExtension(file.FileName); //get the file extension
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        dtExcel = ReadExcel(file.FileName); //read excel file
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = dtExcel;
                        MessageBox.Show(dataGridView1.Rows.Count.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            foreach(DataRow row in dtExcel.Rows)
            {
                Product product = new Product();
                product.SalePrice = 0;
                product.BuyPrice = 0;
                product.itemType = 0;
                product.itemCode = "EG-232389578-" + row[1].ToString();
                product.requestLimit = 0;
                product.CategoryId = 1;
                product.Name = row[2].ToString();
                product.Code = row[1].ToString();
                product.GPCCode = row[3].ToString();
                product.ProductUnites = new List<ProductUnites>()
                {
                    new ProductUnites
                    {
                        UnitId = 1,
                        UnitConvert = 1,
                        QtySmallUnit = 1,
                        BuyPrice = product.BuyPrice,
                        SellPrice = product.SalePrice,
                        Barcode = product.Code,
                        Avg = 0,
                    }
                };
                _unitOfWork.Products.Add(product);
            }
            
        }
    }
}
