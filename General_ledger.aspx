<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="General_ledger.aspx.cs" Inherits="General_ledger" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" /> 
      <style type="text/css">
        .style10
        {
            width: 128px;
        }
          .style12
          {
              width: 156px;
          }
          .style13
          {
              width: 104px;
          }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
<legend class="BigLegend">
<h1 style="position:relative; right:0px;">
General Ledger
</h1>
</legend>
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
     <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
</h3>
<table cellpadding="0" cellspacing="0" align="center">
<tr>
<td align="right" >
<asp:Label ID="lblDay" align="center" runat="server" CssClass="Label" Text="From Date&nbsp;&nbsp;&nbsp;"></asp:Label>
</td>
&nbsp;&nbsp;&nbsp;&nbsp;
<td>
<asp:TextBox runat="server" ID="txtDay" CssClass="TextBox" TabIndex="2" align="center"
        Width="100px" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="txtDay_TextChanged"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDay" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
    </td>
    <td align="right" class="style13"><asp:Label ID="Label1" align="center" runat="server" CssClass="Label" Text="To Date&nbsp;&nbsp;&nbsp;"></asp:Label>
    </td>
<td class="style10">
<asp:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox1" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
<asp:TextBox runat="server" ID="TextBox1" CssClass="TextBox" TabIndex="2" align="center"
        Width="100px" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="TextBox1_TextChanged"></asp:TextBox>
        
    </td>
</tr>
<tr>
   <td align="right">
                        </td>
                       
                           <td>
                                <asp:CheckBox ID="chkallhead" runat="server" TabIndex='4'  AutoPostBack="true" Text="All Head"
                                    CssClass="CheckBox" onkeydown="return(event.keyCode != 13)" 
                                    oncheckedchanged="chkallhead_CheckedChanged"/>
                            </td>
                            <td></td>
                            <td></td>
                            </tr>
                            <tr>
<td align="center"><asp:Label ID="lblGrp" runat="server" Text="Main Head&nbsp;&nbsp;" CssClass="Label"></asp:Label>
</td>
<td class="style12"><asp:DropDownList ID="ddlGrp" runat="server" 
        CssClass="DropDown" TabIndex="2" AutoPostBack="true" 
        onselectedindexchanged="ddlGrp_SelectedIndexChanged"></asp:DropDownList>
       </td>

<td align="right" class="style13">
    <asp:Label ID="Label2" runat="server" Text="Sub Head&nbsp;&nbsp;&nbsp;" CssClass="Label"></asp:Label>
</td>
<td><asp:DropDownList ID="DropDownList1" runat="server" CssClass="DropDown" TabIndex="2" AutoPostBack="true"></asp:DropDownList>
</td>
</tr>
</table>

<div align="center" class="SubmitButtons" style="position:relative; left:450px; top:0px;">
<asp:Button ID="btnSave" runat="server" Text="Generate" Width="95px" 
        CssClass="Buttons embossed-link" TabIndex="11" onclick="btnSave_Click"/>
<asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" 
        CssClass="Buttons embossed-link" tabindex="12" onclick="btnexit_Click"/>
        </div>
       
</fieldset>
    </asp:Content>

