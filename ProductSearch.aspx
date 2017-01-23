<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="ProductSearch.aspx.cs" Inherits="Product_search" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>


 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax"%>
   
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
    <script src="JavaScripts/jquerytab.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

  <script type="text/javascript">


      window.onload = function () {

          setTimeout(ShowMessage(), 1000);

      }

      function ShowMessage() {

          document.getElementById("<%=txtmed.ClientID %>").focus();
          


      }

  </script> 
  <style type="text/css">
.fancy-green .ajax__tab_header
{
background: url(Images/green_bg_Tab.gif) repeat-x;
cursor:pointer;
}
.fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
{
background: url(Images/green_left_Tab.gif) no-repeat left top;
}
.fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
{
background: url(Images/green_right_Tab.gif) no-repeat right top;
}
.fancy .ajax__tab_header
{
font-size: 13px;
font-weight: bold;
color: #000;
font-family: sans-serif;
}
.fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
{
height: 46px;
}
.fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
{
height: 46px;
margin-left: 16px; /* offset the width of the left image */
}
.fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
{
margin: 16px 16px 0px 0px;
}
.fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
{
color: #fff;
}
.fancy .ajax__tab_body
{
font-family: Arial;
font-size: 10pt;
border-top: 0;
border:1px solid #999999;
padding: 8px;
background-color: #ffffff;
}

  .divprint 
        {
            font:15px Arial;
            padding:10px;
        }
</style>

 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style=" width:100%">
       <ajax:TabContainer ID="TabContainer1" runat="server" Width="100%"  
              BackColor="#B69B4C" CssClass="fancy fancy-green" ActiveTabIndex="1" 
              AutoPostBack="true" ActiveTabChanged="TabContainer1_ActiveTabChanged">
       <ajax:TabPanel ID="tbpnlchemical" runat="server" Width="100%" BackColor="#D9D2E9" ToolTip="Tooltip_TabPanel1"  >
      <HeaderTemplate>
Chemical Composition
</HeaderTemplate>

   







<ContentTemplate>
<asp:Panel ID="Panelchemical" runat="server" Width="100%" 
        BackColor="#B69B4C" ><br /><br /><h3><asp:Label ID="lblerror" runat="server" CssClass="ErrorLabel"></asp:Label>
        <asp:Label ID="lblsuccess" runat="server" CssClass="SuccessLabel"></asp:Label></h3>
        <table align="center"><tr><td><asp:Label ID="lblchem" runat="server" Text="Chemical Composition" TabIndex="1"
                   CssClass="Label"></asp:Label></td><td>
                   <asp:TextBox ID="txtchem" runat="server" Width="253px" TabIndex="1"></asp:TextBox>
                   <ajax:AutoCompleteExtender ID="txtchem_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtchem" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass"></ajax:AutoCompleteExtender>
                               </td><td><asp:Button ID="btnchserch" runat="server" Text="Search" Width="95px"
                     CssClass="Buttons embossed-link" onclick="btnchserch_Click"  TabIndex="2"/></td><td><asp:Button ID="btnchem" runat="server" Text="Print" Width="95px" 
                       CssClass="Buttons embossed-link" onclick="btnchem_Click"  TabIndex="3"/></td><td><asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" 
                       CssClass="Buttons embossed-link" onclick="btnexit_Click" TabIndex="4"/></td></tr></table><br /><asp:Panel ID="pnlchem" runat="server" ScrollBars="Auto"><asp:GridView ID="chemdetails" align="center" runat="server" Width="829px" CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found"><AlternatingRowStyle BackColor="#F1F1F1" /><HeaderStyle CssClass="HeaderStyle" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><RowStyle CssClass="RowStyle" BorderColor="White" /></asp:GridView></asp:Panel></asp:Panel>
</ContentTemplate>

           








</ajax:TabPanel>
 
    <ajax:TabPanel ID="tbmed" runat="server"  Width="100%" BackColor="#D9D2E9" >
    <HeaderTemplate>
Search Medicine
</HeaderTemplate>
        
    







<ContentTemplate>
<asp:Panel ID="Panelmedicine" runat="server" Width="100%" BackColor="#b69b4c" ><br /><br />
<table align="center"><caption><h3><asp:Label ID="lblmsucc" runat="server" CssClass="ErrorLabel"></asp:Label>
<asp:Label ID="lblmerror" runat="server" CssClass="SuccessLabel"></asp:Label></h3>
<tr><td>

<asp:Label ID="lblmed" runat="server" CssClass="Label" Text="Medicine Name"></asp:Label>
</td><td>
<asp:TextBox ID="txtmed" runat="server" 
                              Width="253px"></asp:TextBox>
                               <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername10" ServicePath="" TargetControlID="txtmed" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass"></ajax:AutoCompleteExtender>
                              </td><td align="center"><asp:Button ID="btnmed" runat="server" CssClass="Buttons embossed-link" 
                              OnClick="btnmed_Click" Text="Search" Width="95px" /></td><td>

                            <asp:Button ID="btnexitmed" runat="server" CssClass="Buttons embossed-link" 
                              OnClick="btnexitmed_Click" Text="Exit" Width="95px" /></td></tr></caption></table><br /><br /><br /><table><tr><td align="center" width="200px"><asp:Label ID="lblstkhead" runat="server" Text="STOCK" Font-Bold="True"  ForeColor="Black"
                   Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblshelfhead" runat="server" Text="SHELF" Font-Bold="True"  ForeColor="Black"
                   Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblrackhead" runat="server" Text="RACKS" Font-Bold="True"  ForeColor="Black"
                   Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblmrphead" runat="server" Text="MRP" Font-Bold="True"  ForeColor="Black"
                   Font-Size="Large"></asp:Label></td></tr></table><table><tr><td align="center" width="200px"><asp:Label ID="lblstock" runat="server" Font-Bold="True" 
                   Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblshelf" runat="server" Font-Bold="True" 
                      Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblrack" runat="server"  Font-Bold="True" 
                   Font-Size="Large"></asp:Label></td><td align="center" width="200px"><asp:Label ID="lblmrp" runat="server"  Font-Bold="True" 
                   Font-Size="Large"></asp:Label></td></tr></table></asp:Panel>

</ContentTemplate>

        








</ajax:TabPanel>
  <ajax:TabPanel ID="tbgeneric" runat="server" Width="100%" BackColor="#D9D2E9">
    <HeaderTemplate>
Generic Medicines
</HeaderTemplate>
      








<ContentTemplate>
<asp:Panel ID="pnlgeneric" runat="server" Width="100%" 
        BackColor="#B69B4C"><table align="center">
        <caption><br />
            <br /><h3><asp:Label ID="lblsucc" runat="server" CssClass="ErrorLabel"></asp:Label>

<asp:Label ID="lblerro" runat="server" CssClass="SuccessLabel"></asp:Label>

</h3><tr><td><asp:Label ID="lblgenename" runat="server" CssClass="Label" Text="Generic Name"></asp:Label>

</td><td><asp:TextBox ID="txtgenename" runat="server" Width="253px"></asp:TextBox>

<ajax:AutoCompleteExtender ID="txtgenenamer_AutoCompleteExtender" runat="server" 
                        CompletionListCssClass="OtherCompletionCssClass" 
                        CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" 
                        CompletionListItemCssClass="OtherCompletionItemCssClass" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="Buyername1" 
                        ServicePath="" TargetControlID="txtgenename" UseContextKey="True"></ajax:AutoCompleteExtender>

</td><td><asp:Button ID="btngesrch" runat="server" Text="Search" Width="95px"
                     CssClass="Buttons embossed-link" onclick="btngesrch_Click" />

</td><td><asp:Button ID="btngene" runat="server" CssClass="Buttons embossed-link" 
                       OnClick="btngene_Click" Text="Print" Width="95px" />

</td><td><asp:Button ID="btnexitgene" runat="server" CssClass="Buttons embossed-link" 
                       OnClick="btnexitgene_Click" Text="Exit" Width="95px" />

</td></tr></caption></table><br /><asp:Panel ID="pnlgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="genedetails" align="center" runat="server" Width="829px" CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>

</asp:Panel>

</asp:Panel>

</ContentTemplate>
      








</ajax:TabPanel>
</ajax:TabContainer>
</div>  

 
</asp:Content>

