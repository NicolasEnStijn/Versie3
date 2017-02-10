<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="brandcentrales.aspx.cs" Inherits="WebApplication3.brandcentrales" async ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="brandcentrales.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Table ID="Table1" runat="server" >
            <asp:TableRow cssclass="top">
                <asp:TableCell CssClass="open"></asp:TableCell>
                <asp:TableCell CssClass="naam"><asp:Button ID="name" runat="server" cssClass="sort" UseSubmitBehavior="false" Text="naam" onclick="Page_Load" />
                    <asp:TextBox ID="TextBoxNaam" runat="server" OnTextChanged="Page_Load" cssClass="sort" AutoPostBack="True"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell CssClass="stad"><asp:Button ID="municipality" cssclass="sort" UseSubmitBehavior="false" runat="server" Text="stad" OnClick="Page_Load" /><asp:TextBox ID="TextBoxMunicipality" runat="server" cssClass="sort" OnTextChanged="Page_Load" AutoPostBack="true"></asp:TextBox></asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Btnupdate" runat="server" Text="berijkbaarheid" CssClass="icoon" OnClick="check" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
