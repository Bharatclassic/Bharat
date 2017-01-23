<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="DayBookTrans.aspx.cs" Inherits="DayBookTrans" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   <script type="text/javascript">
       function alpha1(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k >= 48 && k <= 57) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
       }
   </script>
    <style type="text/css">
        .style11
        {
            width: 120px;
        }
        .style13
        {
            width: 130px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class = "BigFieldSet">
<legend class = "BigLegend">
<h1 style="position:relative; right:0px;">
DAY BOOK TRANSACTIONS
    </h1>
</legend>
<h3>
    <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
    <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
</h3>
<asp:UpdatePanel ID="Panel1" runat="server">
<ContentTemplate>
<table align="center">
<tr>
<td></td>
<td align="right">
<asp:Label runat="server" CssClass="Label" Text="Transaction Date:" ID="lblTrDate" ></asp:Label>
</td>
<td align="center" class="style13">
<asp:TextBox runat="server" ID="txtTrDate" CssClass="TextBox" Width="120px" AutoPostBack="true"></asp:TextBox>
<asp:CalendarExtender runat="server" ID="calenderextender1" Format="dd-MM-yyyy" TargetControlID="txtTrDate"></asp:CalendarExtender> 
</td>
<td class="style11"></td>
</tr>
<tr>
<td align="right"><asp:Label runat="server" ID="lblMain" CssClass="Label" Text="Main Head:"></asp:Label></td>
<td align="right"><asp:DropDownList Height="25px" CssClass="DropDown" ID="ddlMain" runat="server" AutoPostBack="true" TabIndex="2" Width="170px" 
onselectedindexchanged="ddlMain_SelectedIndexChanged" Font-Size="11.5px"></asp:DropDownList></td>
<td align="right" class="style13"><asp:Label runat="server" ID="lblSubHead" CssClass="Label" Text="Sub Head:"></asp:Label></td>
<td><asp:DropDownList CssClass="DropDown" Font-Size="11.5px" Width="170px" Height="25px" ID="ddlSubHead" runat="server" TabIndex="3"></asp:DropDownList></td>
</tr>
<tr>
<td></td>
<td align="right">
<div align="center" style="width:100px">
<asp:RadioButton ID="rdCred" runat="server" Width="70px" AutoPostBack="true" Font-Size="Medium"
        Text="Credit" CssClass="RadioButton" BackColor="#e6e600"
        TabIndex="4" oncheckedchanged="rdCred_CheckedChanged"/>
</div> 
</td>
<td align="center" class="style13">
<div style="width:110px" align="left">
<asp:RadioButton ID="rdDeb" runat="server" Width="110px" BackColor="#e6e600" AutoPostBack="true" Font-Size="Medium" Text="Debit" CssClass="RadioButton" 
        TabIndex="5" oncheckedchanged="rdDeb_CheckedChanged" />
</div>
</td>
<td class="style11"></td>
</tr>
<tr>
<td></td>
<td align="right">
<div align="center" style="width:100px">
<asp:RadioButton ID="rdCash" runat="server" Width="70px" AutoPostBack="true" Font-Size="Medium"
        Text="Cash" CssClass="RadioButton" BackColor="#e6e600" f
        TabIndex="6" oncheckedchanged="rdCash_CheckedChanged" />
</div> 
</td>
<td align="center" class="style13">
<div align="left" style="width:110px">
<asp:RadioButton ID="rdAdj" TextAlign="Right" runat="server" Width="110px" AutoPostBack="true" Font-Size="Medium"
        Text="Adjustment" CssClass="RadioButton" BackColor="#e6e600" TabIndex="7" oncheckedchanged="rdAdj_CheckedChanged" />
</div> 
</td>
<td class="style11"></td>
</tr>
<tr>
<td><asp:Label ID="lblRec" runat="server" CssClass="Label" Text="Receipt No:"></asp:Label></td>
<td><asp:TextBox runat="server" ID="txtRec" CssClass="TextBox" TabIndex="8" 
        AutoPostBack="true" ontextchanged="txtRec_TextChanged" Width="129px"></asp:TextBox></td>
<td align="right" class="style13"><asp:Label runat="server" ID="lblCheque" CssClass="Label" Text="Cheque No:"></asp:Label></td>
<td class="style11"><asp:TextBox ID="txtCheque" runat="server" CssClass="TextBox" 
        AutoPostBack="true" TabIndex="9" ontextchanged="txtCheque_TextChanged" 
        Width="129px"></asp:TextBox></td>
</tr>
<tr>
<td></td>
<td align="right"><asp:Label ID="lblAmt" runat="server" text="Amount(Rs.)" CssClass="Label"></asp:Label></td>
<td class="style13"><asp:TextBox ID="txtAmt" runat="server" CssClass="TextBox" 
        TabIndex="10" onkeypress="return alpha1(event)" Width="122px"></asp:TextBox></td>
<td class="style11"></td>
</tr>
</table>
</ContentTemplate>
        </asp:UpdatePanel>
<div align="center" class="SubmitButtons" style="position:relative; left:450px; top:0px;">
<asp:Button ID="btnSave" runat="server" Text="Save" Width="95px" 
        CssClass="Buttons embossed-link" TabIndex="11" onclick="btnSave_Click"/>
<asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" 
        CssClass="Buttons embossed-link" onclick="btnexit_Click" tabindex="12"/>
</fieldset>
</asp:Content>


