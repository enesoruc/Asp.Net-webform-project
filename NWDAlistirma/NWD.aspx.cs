using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NWDAlistirma
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        NORTHWNDEntities db;
        public WebForm1()
        {
            db = new NORTHWNDEntities();
        }
        string userName = string.Empty;
        string password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = txtUserName.Text;
            password = txtPassword.Text;
        }

        private void GetOrder(string customerID,string employee)
        {
            lstOrders.DataSource = db.Employees.Join(db.Orders,
                                                      emp => emp.EmployeeID,
                                                      o => o.EmployeeID,
                                                      (emp, o) => new
                                                      {
                                                          MusteriID = o.CustomerID,
                                                          calisan = emp.FirstName + "." + emp.LastName,
                                                          siparisID = o.OrderID,
                                                          siparisTarih = o.OrderDate,
                                                      }).Join(db.Customers,
                                                               empo => empo.MusteriID,
                                                               c => c.CustomerID,
                                                               (empo, c) => new
                                                               {
                                                                   MusteriID = empo.MusteriID,
                                                                   calisanAd = empo.calisan,
                                                                   siparisID = empo.siparisID,
                                                                   siparisTarihi = empo.siparisTarih,
                                                               }).Join(db.Order_Details,
                                                                        empoc => empoc.siparisID,
                                                                        od => od.OrderID,
                                                                        (empoc, od) => new
                                                                        {
                                                                            musteriID = empoc.MusteriID,
                                                                            urun = od.ProductID,
                                                                            calisanAd = empoc.calisanAd,
                                                                            siparisID = empoc.siparisID,
                                                                            siparisTarih = empoc.siparisTarihi,
                                                                        }).Join(db.Products,
                                                                                empoc => empoc.urun,
                                                                                p => p.ProductID,
                                                                                (empoc, p) => new
                                                                                {
                                                                                    musteriID = empoc.musteriID,
                                                                                    siparisNo = empoc.siparisID,
                                                                                    siparisTarihi = empoc.siparisTarih,
                                                                                    urunAdi = p.ProductName,
                                                                                    calisanAd = empoc.calisanAd,
                                                                                }).Where(a => a.calisanAd == userName && a.musteriID == customerID).Select(a => new { a.siparisNo, a.siparisTarihi, a.musteriID, a.urunAdi }).ToList();
            lstOrders.DataBind();
        }

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (IsPostBack)//sayfa daha önceden yüklendiyse
            {
                userName = txtUserName.Text;
                password = txtPassword.Text;
            }
            ddlCustomers.DataSource = db.Employees.Join(db.Orders,
                                                      emp => emp.EmployeeID,
                                                      o => o.EmployeeID,
                                                      (emp, o) => new
                                                      {
                                                          MusteriID = o.CustomerID,
                                                          UserName = emp.FirstName + "." + emp.LastName,
                                                          Password=emp.LastName+"."+emp.BirthDate.Value.Year,

                                                      }).Join(db.Customers,
                                                               empo => empo.MusteriID,
                                                               c => c.CustomerID,
                                                               (empo, c) => new
                                                               {
                                                                   Musteri = c.CompanyName,
                                                                   MusteriID=c.CustomerID,
                                                                   UserName = empo.UserName,
                                                                   Password=empo.Password,
                                                               }).Where(a => a.UserName == userName && a.Password==password).Select(a =>new { a.Musteri,a.MusteriID }).ToList();
            ddlCustomers.DataTextField = "Musteri"; // DisplayMember
            ddlCustomers.DataValueField = "MusteriID"; // ValueMember
            ddlCustomers.DataBind();
            if (ddlCustomers.DataMember=="")
            {
                lblInfo.Text = "girilen bilgiler hatalı";
                return;
            }
        }

        protected void ddlCutomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerID = ddlCustomers.SelectedValue;
            GetOrder(customerID, userName);            
        }
    }
}