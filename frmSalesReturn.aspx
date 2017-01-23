<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="frmSalesReturn.aspx.cs" Inherits="frmSalesReturn" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

     <script type="text/javascript">
         function HideLabel() {
             var seconds = 3;
             setTimeout(function () {
                 document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
             }, seconds * 1000);
         };
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
 <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Sales Return
    </h1>
    </legend>
     
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>

    <asp:Panel ID="Panel2" runat="server" Width="1000px" > 
        <div align="center">
          <asp:Label ID="lblbillno" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Bill No:"></asp:Label>
        
               <asp:TextBox ID="txtbillno" runat="server"  AutoPostBack="true" OnTextChanged="txtbillno_TextChanged"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" ></asp:TextBox>
                                                  
          </div>
          <br />
          <br />
          <div align="center">
          
          
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="Green">
        <Columns>
            <asp:BoundField DataField="Product Name" HeaderText="Product Name" ItemStyle-Width="150" />
            <asp:BoundField DataField="Batch No" HeaderText="Batch No" ItemStyle-Width="40" />
            <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" ItemStyle-Width="60" />
            <asp:BoundField DataField="Sales_Qty" HeaderText="Sales_Qty" ItemStyle-Width="50" />
            <asp:BoundField DataField="Sales_Price" HeaderText="Sales_Price" ItemStyle-Width="50" />
            
           
             <asp:TemplateField HeaderText="Return_Qty">
<ItemTemplate>
<asp:TextBox ID="Return_Qty" runat="server" BackColor="Orange" Text='<%# Eval("Return_Qty") %>' OnTextChanged="Return_Qty_TextChanged" Width="50" AutoPostBack="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Totalamt" HeaderText="Totalamt" ItemStyle-Width="50" />
        </Columns>
    </asp:GridView>
    </div>
    </asp:Panel>
    <div align="center" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" TabIndex="2" 
                 CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" onclick="btnExit_Click"
                 CssClass="Buttons embossed-link" TabIndex="3"/>  
                </div> 
     </fieldset>  
</asp:Content>

