<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Custsalerpt.aspx.cs" Inherits="Custsalerpt" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
        <legend class="BigLegend">
            <h1>
                Pharmacy Collection Report
            </h1>
        </legend>
        <h3>
            <asp:Label ID="lblError" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
            <asp:Label ID="lblSuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
        </h3>
        <table align="center" width="600px">
           
            <tr>
                <td id="Branch" align="center" runat="server">
                    <asp:Label ID="lblbranchname" runat="server" Text="Customer Name :" CssClass="Label"></asp:Label>
                    <asp:TextBox ID="txtcustname" runat="server" width="253px" AutoPostBack="true"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"
              TabIndex="4"></asp:TextBox>
                </td>
            </tr>
        </table>
      
        <asp:GridView ID="grdTransDetails" runat="server" align="center" Width="600px" CellPadding="4"
            BackColor="White" GridLines="Both">
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle CssClass="RowStyle" BorderColor="#fbfbfb" />
            <HeaderStyle CssClass="HeaderStyle" />
            <AlternatingRowStyle BackColor="#f1f1f1" />
        </asp:GridView>
        <div class="SubmitButtons" style="position: relative; left: 80px;">
            <asp:Button ID="btnSalesRegGenerate" runat="server" Text="Generate" CssClass="buttonsDepartment"/>
            <asp:Button ID="btnSalesRegPrint" runat="server" Text="Print" 
                CssClass="buttonsDepartment" onclick="btnSalesRegPrint_Click"/>
            <asp:Button ID="btnSalesRegExit" runat="server" Text="Exit" CssClass="buttonsDepartment"/>
        </div>
    </fieldset>
</asp:Content>

