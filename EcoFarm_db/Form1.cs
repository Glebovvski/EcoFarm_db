using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoFarm_db
{
    public partial class EcoFarm : Form
    {
        private bool linked = false;

        DataGridViewRow row = new DataGridViewRow();

        public EcoFarm()
        {
            InitializeComponent();
        }

        private void invoiceBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.invoiceBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ecoFarm_DBDataSet);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ecoFarm_DBDataSet.Invoice_products' table. You can move, or remove it, as needed.
            this.invoice_productsTableAdapter.Fill(this.ecoFarm_DBDataSet.Invoice_products);
            // TODO: This line of code loads data into the 'ecoFarm_DBDataSet.Invoice' table. You can move, or remove it, as needed.
            this.invoiceTableAdapter.Fill(this.ecoFarm_DBDataSet.Invoice);


        }

        private void invoiceBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            if (SortCB.SelectedIndex < 0)
                SortCB.SelectedIndex = 0;
            if (SortDir.SelectedIndex < 0)
                SortDir.SelectedIndex = 0;
            invoiceDataGridView.Sort(invoiceDataGridView.Columns[SortCB.SelectedIndex], SortDir.SelectedIndex == 0 ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            string filter = SortCB.SelectedText;
            invoiceBindingSource.Filter = filter + " = '" + FilterTB.Text + "'";
        }

        private void SortButtonProd_Click(object sender, EventArgs e)
        {
            if (SortCBP.SelectedIndex < 0)
                SortCBP.SelectedIndex = 0;
            if (SortDirProd.SelectedIndex < 0)
                SortDirProd.SelectedIndex = 0;
            invoice_productsDataGridView.Sort(invoice_productsDataGridView.Columns[SortCBP.SelectedIndex], SortDirProd.SelectedIndex == 0 ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private void LinkButton_Click(object sender, EventArgs e)
        {
            if (linked == false)
            {
                row = invoiceDataGridView.SelectedRows[0];
                invoice_productsDataGridView.DataSource = ((DataRowView)invoiceDataGridView.SelectedRows[0].DataBoundItem).Row.GetChildRows("Invoice_Invoice products").CopyToDataTable();
                linked = true;
            }
            else
            {
                if (invoiceDataGridView.SelectedRows[0] != row)
                {
                    invoice_productsDataGridView.DataSource = ((DataRowView)invoiceDataGridView.SelectedRows[0].DataBoundItem).Row.GetChildRows("Invoice_Invoice products").CopyToDataTable();
                    row = invoiceDataGridView.SelectedRows[0];
                }
                else
                {
                    invoice_productsDataGridView.DataSource = invoice_productsBindingSource;
                    linked = false;
                }
            }
        }

        private void FilterBtnProd_Click(object sender, EventArgs e)
        {
            string filter = SortCBP.SelectedText;
            invoice_productsBindingSource.Filter = filter + " = '" + FilterTBP.Text + "'";
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {

        }

        private void Unlink_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            invoiceDataGridView.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 15, FontStyle.Bold);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.MoveFirst();
            //invoice_productsDataGridView.ClearSelection();
            //invoice_productsDataGridView.CurrentCell = invoice_productsDataGridView.Rows[0].Cells[0];
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.MovePrevious();
            //int prev = invoice_productsDataGridView.CurrentRow.Index - 1;
            //if (prev >= 0)
            //    invoice_productsDataGridView.CurrentCell = invoice_productsDataGridView.Rows[prev].Cells[invoice_productsDataGridView.CurrentCell.ColumnIndex];
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.MoveNext();
            //int next = invoice_productsDataGridView.CurrentRow.Index + 1;
            //if (next < invoice_productsDataGridView.RowCount)
            //    invoice_productsDataGridView.CurrentCell = invoice_productsDataGridView.Rows[next].Cells[invoice_productsDataGridView.CurrentCell.ColumnIndex];
        }

        private void LastBtn_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.MoveLast();
            //int last = invoice_productsDataGridView.Rows.Count-2;
            //invoice_productsDataGridView.CurrentCell = invoice_productsDataGridView.Rows[last].Cells[0];
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            invoice_productsDataGridView.Rows.Add();

        }

        private void FindByPKBtn_Click(object sender, EventArgs e)
        {
            bool success = int.TryParse(FindByPKTxt.Text, out int pk);
            if (!success)
            {
                MessageBox.Show("Input must be digit");
                FindByPKTxt.Text = String.Empty;
            }
            else
            {
                invoiceBindingSource.Position = invoiceBindingSource.Find("Invoice number", pk);
            }
        }

        private void FindByPKProd_Click(object sender, EventArgs e)
        {
            bool success = int.TryParse(FindByPKTxtProd.Text, out int pk);
            if (!success)
            {
                MessageBox.Show("Input must be digit");
                FindByPKTxtProd.Text = String.Empty;
            }
            else
            {
                invoice_productsBindingSource.Position = invoice_productsBindingSource.Find("Product code", pk);
            }
        }

        private void FindBtn_Click(object sender, EventArgs e)
        {
            string find = FindTxt.Text;
            if (SortCB.SelectedIndex < 0)
                SortCB.SelectedIndex = 0;
            string param = SortCB.SelectedText;
            invoiceBindingSource.Position = invoiceBindingSource.Find(param, find);
        }

        private void FindBtnProd_Click(object sender, EventArgs e)
        {
            string find = FindProdTxt.Text;
            if (SortCBP.SelectedIndex < 0)
                SortCBP.SelectedIndex = 0;
            string param = SortCBP.SelectedText;
            invoice_productsBindingSource.Position = invoice_productsBindingSource.Find(param, find);
        }

        private void invoiceDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //InvNumberTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            DateTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            SupplierCodeTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            InvoiceTypeTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            SupplierTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            RecieverTxt.Text = invoiceDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                invoiceDataGridView.SelectedRows[0].Cells[1].Value = DateTime.Parse(DateTxt.Text);
                invoiceDataGridView.SelectedRows[0].Cells[2].Value = int.Parse(SupplierCodeTxt.Text);
                invoiceDataGridView.SelectedRows[0].Cells[3].Value = int.Parse(InvoiceTypeTxt.Text);
                invoiceDataGridView.SelectedRows[0].Cells[4].Value = SupplierTxt.Text;
                invoiceDataGridView.SelectedRows[0].Cells[5].Value = RecieverTxt.Text;

                this.Validate();
                this.invoiceBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.ecoFarm_DBDataSet);

                //invoiceTableAdapter.Update(ecoFarm_DBDataSet.Invoice);
                //tableAdapterManager.UpdateAll(ecoFarm_DBDataSet);
                //ecoFarm_DBDataSet.AcceptChanges();
            }
            catch
            {
                MessageBox.Show("Make sure your input is of correct data type");
            }
        }

        private void invoiceDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Make sure your input is of correct data type");
        }

        private void invoice_productsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ProdCodeTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            NameTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            UnitsTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            NumUnitsTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            UnitPriceTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            TotalPriceTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            InvoiceNumTxt.Text = invoice_productsDataGridView.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void SaveProdBtn_Click(object sender, EventArgs e)
        {
            try
            {
                invoice_productsDataGridView.SelectedRows[0].Cells[0].Value = invoice_productsDataGridView.RowCount;
                invoice_productsDataGridView.SelectedRows[0].Cells[1].Value = NameTxt.Text;
                invoice_productsDataGridView.SelectedRows[0].Cells[2].Value = UnitsTxt.Text;
                invoice_productsDataGridView.SelectedRows[0].Cells[3].Value = float.Parse(NumUnitsTxt.Text);
                invoice_productsDataGridView.SelectedRows[0].Cells[4].Value = float.Parse(UnitPriceTxt.Text);
                invoice_productsDataGridView.SelectedRows[0].Cells[5].Value = float.Parse(TotalPriceTxt.Text);
                invoice_productsDataGridView.SelectedRows[0].Cells[6].Value = int.Parse(InvoiceNumTxt.Text);

                this.Validate();
                this.invoice_productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.ecoFarm_DBDataSet);
            }
            catch
            {
                MessageBox.Show("Make sure your input is of correct data type");
            }
        }

        private void invoice_productsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Make sure your input is of correct data type");
        }

        private void invoice_productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void invoiceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddProdRow_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.AddNew();
        }

        private void SaveProdBtnNav_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.invoiceBindingSource.EndEdit();
            this.invoice_productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ecoFarm_DBDataSet);
        }

        private void DeleteProdBtn_Click(object sender, EventArgs e)
        {
            invoice_productsDataGridView.Rows.Remove(invoice_productsDataGridView.SelectedRows[0]);
            this.tableAdapterManager.UpdateAll(this.ecoFarm_DBDataSet);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            invoiceBindingSource.RemoveFilter();
        }

        private void ClearProdBtn_Click(object sender, EventArgs e)
        {
            invoice_productsBindingSource.RemoveFilter();
        }
    }
}
