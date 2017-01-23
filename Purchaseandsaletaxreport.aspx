<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Purchaseandsaletaxreport.aspx.cs" Inherits="Purchaseandsaletaxreport" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<legend class="BigLegend">
<h1 style="position:relative; right:0px;">
Purchase and Sales Tax Report
</h1>
</legend>
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel" ></asp:Label>
    </h3>
     <table id="table" align="center">
      <tr align='center'>
<td style="width:33px"><asp:Label ID="lblBtwDate1" Width="75px" runat="server" 
        CssClass="Label" Text="From Date" align="right"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtbtwDate1" Width="102px" cssclass="TextBox" runat="server" 
            align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" 
            AutoComplete="off" TabIndex="1" ></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtbtwDate1" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td class="style10">
<asp:Label ID="lblBtwDate2" Width="60px" runat="server" CssClass="Label" 
        Text="To Date" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtbtwDate2" cssclass="TextBox" runat="server" TabIndex="2"
         Width="100px" align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtbtwDate2" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
</td>
</tr>
      </table>  
     
       <div align="Left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
                          <asp:Button ID="btnreport" runat="server" Text="Report" Width="95px" TabIndex="3"
                        CssClass="Buttons embossed-link" onclick="btnreport_Click" />
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" TabIndex="4" 
                        CssClass="Buttons embossed-link" onclick="btnexit_Click"/>
 
                         </div>     
                          <div style="width: 830px; margin: 0 auto;">
            <div style="float: left;">
                <h2 style="text-align: center; color: Black">
                    </h2>
                <asp:GridView ID="grdpurchasetax" runat="server" align="center" Width="415px"
                    CellPadding="4" BackColor="White" GridLines="Both">
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle CssClass="RowStyle" BorderColor="#fbfbfb" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                </asp:GridView>
            </div>
            <div style="float: right;">
                <h2 style="text-align: center; color: Black">
                    </h2>
                <asp:GridView ID="grdsaletax" runat="server" align="center" Width="415px"
                    CellPadding="4" BackColor="White" GridLines="Both">
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle CssClass="RowStyle" BorderColor="#fbfbfb" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                </asp:GridView>
            </div>
        </div>                                   
</fieldset>   
</asp:Content>

