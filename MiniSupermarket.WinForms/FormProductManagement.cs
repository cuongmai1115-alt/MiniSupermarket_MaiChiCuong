using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private List<ProductDto> _productList = new List<ProductDto>();
        private List<CategoryDto> _categoryList = new List<CategoryDto>();
        private int _selectedProductId = 0;

        public FormProductManagement()
        {
            InitializeComponent();
            SetupGrid();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 70 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Barcode", HeaderText = "Mã Vạch", Width = 120 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoryName", HeaderText = "Danh Mục", Width = 130 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Đơn Giá", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockQuantity", HeaderText = "Tồn Kho", Width = 90 });
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories");
                if (categories != null)
                {
                    _categoryList = categories;
                    cbCategory.DataSource = _categoryList;
                    cbCategory.DisplayMember = "CategoryName";
                    cbCategory.ValueMember = "CategoryId";
                    cbCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var products = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>("products");
                if (products != null)
                {
                    _productList = products;
                    dgvProducts.DataSource = null;
                    dgvProducts.DataSource = _productList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.Rows[e.RowIndex].DataBoundItem is ProductDto p)
            {
                _selectedProductId = p.ProductId;
                txtBarcode.Text = p.Barcode;
                txtProductName.Text = p.ProductName;
                txtPrice.Text = p.Price.ToString("F0");
                numStock.Value = p.StockQuantity;

                // Đọc linh hoạt thuộc tính CategoryId hoặc CategoryName từ ProductDto nếu có
                int catId = GetPropertyValue<int>(p, "CategoryId");
                string catName = GetPropertyValue<string>(p, "CategoryName");

                if (catId > 0)
                {
                    cbCategory.SelectedValue = catId;
                }
                else if (!string.IsNullOrEmpty(catName))
                {
                    cbCategory.Text = catName;
                }
                else
                {
                    cbCategory.SelectedIndex = -1;
                }
            }
        }

        private T GetPropertyValue<T>(object obj, string propertyName)
        {
            if (obj == null) return default;
            PropertyInfo prop = obj.GetType().GetProperty(propertyName);
            if (prop != null && prop.CanRead)
            {
                var val = prop.GetValue(obj, null);
                if (val != null && val is T tVal) return tVal;
            }
            return default;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int categoryId = cbCategory.SelectedValue != null ? Convert.ToInt32(cbCategory.SelectedValue) : 0;

            var newProduct = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                CategoryId = categoryId,
                Price = decimal.Parse(txtPrice.Text.Trim()),
                StockQuantity = (int)numStock.Value
            };

            var response = await ApiClientService.Client.PostAsJsonAsync("products", newProduct);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
                await LoadProductsAsync();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            int categoryId = cbCategory.SelectedValue != null ? Convert.ToInt32(cbCategory.SelectedValue) : 0;

            var updateProduct = new
            {
                ProductId = _selectedProductId,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                CategoryId = categoryId,
                Price = decimal.Parse(txtPrice.Text.Trim()),
                StockQuantity = (int)numStock.Value
            };

            var response = await ApiClientService.Client.PutAsJsonAsync($"products/{_selectedProductId}", updateProduct);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
                await LoadProductsAsync();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var response = await ApiClientService.Client.DeleteAsync($"products/{_selectedProductId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInput();
                    await LoadProductsAsync();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInput();

        private void ClearInput()
        {
            _selectedProductId = 0;
            txtBarcode.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            numStock.Value = 0;
            cbCategory.SelectedIndex = -1;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và mã vạch sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cho sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}