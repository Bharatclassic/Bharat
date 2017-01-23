<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Customer_Report.aspx.cs" Inherits="Customer_Report" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript">
         $("input [type=radio]").click(function () {
             clickedstate = $(this).attr('clicked');
             $(this).parent('div').children('.radiobutton:clicked').each(function () {
                 $(this).attr('clicked', false);
             });
             $(this).attr('clicked', clickedstate);
         });
     </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<legend class="BigLegend">
<h1 style="position:relative; right:0px;">
Customer Report
</h1>
</legend>
<table align="center" cellspacing="6">
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
</h3>
</table>
<table id="table2" cellpadding="10" style=" cellspacing="10"  width="50%" align="center">
<tr>
<td align="right">
<asp:Label ID="date1" runat="server" Text="Date" CssClass="Label" TabIndex="1"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtdate" CssClass="TextBox" TabIndex="2"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="yyyy/MM/dd" runat="server">
                            </asp:CalendarExtender>
</td>
</tr>
<tr>
<td></td>
<td>
<div>
&nbsp;&nbsp;
</div>
</td>
</tr>
</table>

<div align="center" class="SubmitButtons" style="position:relative; left: 250px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Generate"  Width="95px" 
                 TabIndex="3" CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                 CssClass="Buttons embossed-link" TabIndex="4" onclick="btnExit_Click"/>  
                </div>  
</fieldset>
</asp:Content>
