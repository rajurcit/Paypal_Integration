<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paypal_Websites.aspx.cs" Inherits="Paypal_Integration.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     #creditCardTable tbody > tr + tr + tr + tr + tr {

display:none !important;

}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="700" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="60">
                    <b>Paypal Integration in ASP.NET</b>
                </td>
            </tr>
            <tr>
                <td height="40" align="center">
                    <asp:GridView ID="gvPayPal1" runat="server" AutoGenerateColumns="False"  OnRowCommand="gvPayPal_RowCommand"
                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="gvPayPal1_SelectedIndexChanged">
                        <RowStyle ForeColor="#8C4510" BackColor="#FFF7E7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Product Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("prodName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("prodDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product price">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductPrice" runat="server" Text='<%#Eval("prodPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Buy Now">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/buy.png"
                                       Width="64" Height="64" CommandName="buy" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <!-- PayPal Logo -->
        <table border="0" cellpadding="10" cellspacing="0" align="center">
            <tr>
                <td align="center">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a style="cursor:pointer;" title="Paypal payment gateway center" onclick="javascript:window.open('https://www.paypal.com/cgi-bin/webscr?cmd=xpt/Marketing/popup/OLCWhatIsPayPal-outside','olcwhatispaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=400, height=350');">
                        <img src="https://www.paypal.com/en_US/i/bnr/horizontal_solution_PPeCheck.gif" border="0"
                            alt="Solution Graphics"></a>
                </td>
            </tr>
        </table>
        <!-- PayPal Logo -->
    </div>


    </form>
</body>
</html>
