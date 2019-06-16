<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWD.aspx.cs" Inherits="NWDAlistirma.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div style="text-align:center;position:relative;width:100%;background-color:red;margin:auto">
            <table style="text-align:center;position:relative;margin:auto">
                <tr>
                    <td>Kullanıcı Adı :</td>
                    <td> <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                    <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Kullanıcı Adı Alanı Boş Geçilemez" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Şifre :</td>
                    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Şifre Alanı Boş Geçilemez" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblInfo" runat="server" Text=""></asp:Label></td>
                    
                    <td><asp:Button ID="btnSearch" runat="server" Text="Giriş" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
            <div style="background-color:aqua">
            <asp:DropDownList ID="ddlCustomers" runat="server" OnSelectedIndexChanged="ddlCutomers_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
            <hr />
            <asp:GridView ID="lstOrders" runat="server" AutoGenerateColumns="true"></asp:GridView>
                </div>
        </div>
    </form>
</body>
</html>
