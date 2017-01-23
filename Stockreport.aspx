<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Stockreport.aspx.cs" Inherits="Stockreport" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">

    function alpha(e) {
        var k = e.charCode ? e.charCode : e.keyCode;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
    }

    function HideLabel() {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function toUpper(txt) {
        document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
        return true;
    }

</script>
    <style type="text/css">
        .style9
        {
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Stock Report
    </h1>
    </legend>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel" ></asp:Label>
    </h3>
    <table align="center">
<tr>
<td><asp:RadioButton ID="rdsup" runat="server" Text="SupplierWise" 
        AutoPostBack="true" CssClass="RadioButton" 
        oncheckedchanged="rdsup_CheckedChanged"/></td>
        <td><asp:RadioButton ID="rdprod" runat="server" Text="Product Wise" 
        AutoPostBack="true" CssClass="RadioButton" 
                oncheckedchanged="rdprod_CheckedChanged"/></td>
                <td><asp:RadioButton ID="chkexpiry" runat="server" Text="On Expiry date" 
                AutoPostBack="true" CssClass="RadioButton" 
                        oncheckedchanged="chkexpiry_CheckedChanged" /></td>
                        <td><asp:RadioButton ID="rdtotal" runat="server" Text="Total Stock" 
                AutoPostBack="true" CssClass="RadioButton" oncheckedchanged="rdtotal_CheckedChanged"/></td>
</tr>
</table>
<asp:Panel ID="PanelSupp" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="Table2" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td><asp:Label ID="lblSuppCode" Width="99px" runat="server" 
        CssClass="Label" Text="Supplier Code" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtSuppCode" Width="102px" cssclass="TextBox" runat="server" 
        align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="txtSuppCode_TextChanged"></asp:TextBox>
<asp:AutoCompleteExtender ID="txtSuppCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Suppliercode" ServicePath="" TargetControlID="txtSuppCode" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
<td class="style10">
<asp:Label ID="lblSuppName" Width="108px" runat="server" CssClass="Label" 
        Text="Supplier Name" align="right"></asp:Label>
</td>
<td> 
<asp:TextBox ID="txtSuppName" cssclass="TextBox" runat="server" Width="200px" 
        align="left" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="txtSuppName_TextChanged" ></asp:TextBox>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Suppliername" ServicePath="" TargetControlID="txtSuppName" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="Panel1" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="Table1" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td><asp:Label ID="Label5" Width="99px" runat="server" 
        CssClass="Label" Text="Product Code" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox5" Width="102px" cssclass="TextBox" runat="server" 
        align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="TextBox5_TextChanged" ></asp:TextBox>
<asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="productcode" ServicePath="" TargetControlID="TextBox5" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
<td class="style10">
<asp:Label ID="Label6" Width="108px" runat="server" CssClass="Label" 
        Text="Product Name" align="right"></asp:Label>
</td>
<td> 
<asp:TextBox ID="TextBox6" cssclass="TextBox" runat="server" Width="200px" 
        align="left" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="TextBox6_TextChanged" ></asp:TextBox>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="productname" ServicePath="" TargetControlID="TextBox6" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
</tr>
</table>
</asp:Panel>
    <table id="table" align="center">
         <tr>
         <td  >
                  <asp:Label ID="Label1" Text="Date:" runat="server" CssClass="Label"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtdate" runat="server" CssClass="TextBox" AutoPostBack="true" 
                        onkeypress="return alpha(event)" Width="100px" onkeyup="return toUpper(this.id)"  ></asp:TextBox>
                        <asp:CalendarExtender ID="Calenderext" TargetControlID="txtdate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
            
                </td>
                
                 <table align="center">
                          <tr>
                           <td class="style9">
                        <asp:Label ID="lblexpire" runat="server" Text="On Expiry Date" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                         <br />
                        </td>
                       
                           <td align="left">
                                <asp:CheckBox ID="chkexpiry1" runat="server"  AutoPostBack="true" 
                                    CssClass="CheckBox"  onkeydown="return(event.keyCode != 13)"/>
                              
                            </td>
                            </table>
                             <div align="Left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
                          <asp:Button ID="btnreport" runat="server" Text="Report" Width="95px" 
                        CssClass="Buttons embossed-link" onclick="btnreport_Click" />
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  Tabindex="3"
                        CssClass="Buttons embossed-link" onclick="btnexit_Click"/>
 
                         </div>
                            </tr>
                </table>
                          <asp:Panel ID="pdgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grstockdetails" align="center" runat="server" Width="829px" 
              CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
   </asp:Panel> 


     </fieldset> 
    </table>
</asp:Content>



