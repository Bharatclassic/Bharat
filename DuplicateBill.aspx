<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="DuplicateBill.aspx.cs" Inherits="DuplicateBill" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
        
   <script type="text/javascript">
       $(function () {
           $("[id$=txtgroup]").autocomplete({
               source: function (request, response) {
                   $.ajax({
                       url: '<%=ResolveUrl("~/Group.aspx/GetCustomers") %>',
                       data: "{ 'prefix': '" + request.term + "'}",
                       dataType: "json",
                       type: "POST",
                       contentType: "application/json; charset=utf-8",
                       success: function (data) {
                           response($.map(data.d, function (item) {
                               return {
                                   label: item.split('-')[0],
                                   val: item.split('-')[1]
                               }
                           }))
                       },
                       error: function (response) {
                           alert(response.responseText);
                       },
                       failure: function (response) {
                           alert(response.responseText);
                       }
                   });
               },
               select: function (e, i) {
                   $("[id$=hfCustomerId]").val(i.item.val);
               },
               minLength: 1
           });
       });

       function alpha(e) {
           var k;
           document.all ? k = e.keyCode : k = e.which;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57) || (k <= 09));
       }

       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }


       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class = "BigFieldSet">
<legend class = "BigLegend">
<h1 style="position:relative; right:0px;">
Duplicate Bill
    </h1>
</legend>
<h3>
    <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
    <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
</h3>
    <table id="table1" align="center" runat="server" cellspacing="5">
    <tr>
        <td align="right">
            <asp:Label ID="lblbill" runat="server" Text="Bill No." CssClass="Label"></asp:Label>
        </td>
       <td align="left">
        <asp:TextBox ID="txtbill" runat="server" CssClass="TextBox" Width="150px" onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"></asp:TextBox>
       </td>
    </tr>
    </table>

      <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;">  
                <asp:Button ID="Button1" runat="server" Text="Print" Width="95px" CssClass="Buttons embossed-link" TabIndex="2"/>
               <asp:Button ID="Button2" runat="server"  Text="Exit" Width="90px" CssClass="Buttons embossed-link" TabIndex="3"/>
            </div>
</fieldset>
</asp:Content>

