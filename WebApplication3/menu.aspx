<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="WebApplication3.menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="menu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="hovergrid five-col-grid">
            <article onclick="window.open('brandcentrales.aspx');">
          <asp:Image ID="Image1" src="http://www.ardovlam.be/uploads/Branddetectie%20en%20gasdetectie_opls_opls.png" alt="Branddetectie" runat="server"  />
            <div class="hovercontent">
				<div class="center-table"><div class="center-cell">
					<h2>
						<span>Branddetectie</span>
					</h2>
				</div></div>
			</div>
         </article>
		
		  <article onclick="window.open('camera.aspx');">	
			<asp:Image ID="Image2" src="http://www.ardovlam.be/uploads/Inbraakdetectie_opls_opls.png" alt="Inbraakdetectie"  runat="server"  />
			<div class="hovercontent">
				<div class="center-table"><div class="center-cell">
					<h2>
						<span>Inbraakdetectie</span>
					</h2>
				</div></div>
			</div>
			      </article>
    </div>
    </form>
</body>
</html>
