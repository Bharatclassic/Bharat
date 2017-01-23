<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Supplier_accno.aspx.cs" Inherits="Supplier_accno" %>
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

     function alpha(e) {
         var k = e.charCode ? e.charCode : e.keyCode;
         return ((k > 48 && k < 57) || (k > 96 && k < 123) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);  //k=9(keycode for tab)
     }
     function toUpper(txt) {
         document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
         return true;
     }

     function alpha1(e) {
         var k = e.charCode ? e.charCode : e.keyCode;
         return ((k >= 48 && k <= 57) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39 || k == 46);    //k=9(keycode for tab) ||
     }
       </script>
    <style type="text/css">
        .style9
        {
            height: 133px;
        }
        .style10
        {
            height: 24px;
        }
        #tbl1
        {
            width: 234px;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
<legend class="BigLegend">
 <h1 style="position: relative; right: 0px;">
Supplier Account
</h1>
</legend>

  
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
                </h3>
               

                <table id="tbl" align="left">
                <tr>
                <td>
                <asp:Label ID="lbldate" Text="Date :" runat="server" CssClass="Label"></asp:Label>
               &nbsp;<asp:TextBox ID="txtdate" runat="server" AutoPostBack="true" Width="80px"></asp:TextBox>
                    &nbsp;
               
                
                </td>
                </tr>
                </table>
                <div id="GRN" runat="server" style="float: right; width: 200px;  border: 1px solid gray; height: auto;">
       
       
    <table id="tbl1">
    <tr>
                <td class="style10">
                    </td>
                <td class="style10">
                <asp:Label ID="lblbal" Text="Credit Limit" runat="server" CssClass="Label"></asp:Label>
                &nbsp;
                 <asp:Image ID="image3" runat="server" ImageUrl="~/Images/rupees.JPG" />
                  <asp:Label ID="txtbal1" runat="server" CssClass="Label"></asp:Label>
                <asp:Label ID="txtbal" runat="server" CssClass="Label"></asp:Label>
                </td>     
                </td>
                <td class="style10">
                <asp:Label ID="lblDR" runat="server" CssClass="Label" Text="DR"></asp:Label>
                </td>
                 <td class="style10">
                <asp:Label ID="lblCR" runat="server" CssClass="Label" Text="CR"></asp:Label>
                </td>           
                </tr>
                 <tr>
                <td class="style10">
                    </td>
                
                <td class="style10">
                <asp:Label ID="Label1" Text="Credit Used" runat="server" CssClass="Label"></asp:Label>
                 <asp:Image ID="image1" runat="server" ImageUrl="~/Images/rupees.JPG" />
                &nbsp;
                <asp:Label ID="Label2" runat="server" CssClass="Label"></asp:Label>
                </td>                
                </tr>
                 <tr>
                <td class="style10">
                    </td>
                
                <td class="style10">
                <asp:Label ID="Label3" Text="Balance" runat="server" CssClass="Label"></asp:Label>
                 <asp:Image ID="image2" runat="server" ImageUrl="~/Images/rupees.JPG" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" CssClass="Label"></asp:Label>
                </td>                
                </tr>
    </table>
    </div>


               
                <table id="table" align="center">
               <tr>
               <td>
               <br />
               <br />
                <br />
               <br />
               </td>
                <td align="left">
                <asp:Label ID="lblsupcode" Text="Supplier Code:" runat="server" CssClass="Label"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtsupcode" runat="server" CssClass="TextBox" Width="80px" AutoPostBack="true" onkeypress="return alpha1(event)"
                        ontextchanged="txtsupcode_TextChanged"></asp:TextBox>
                        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                <asp:AutoCompleteExtender ID="txtsupcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Suppliercode" ServicePath="" TargetControlID="txtsupcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                
                </td>
            
                <td>
                <asp:Label ID="lblsupname" Text="Supplier Name:" runat="server" CssClass="Label">
                </asp:Label>
                
                </td>
                
                <td>
                <asp:TextBox ID="txtsupname" runat="server" CssClass="TextBox" width="200px"  AutoPostBack="true" onkeypress="return alpha(event)" onkeyup="return toUpper(this.id)" 
                        ontextchanged="txtsupname_TextChanged" ></asp:TextBox>
                        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                <asp:AutoCompleteExtender ID="txtsupname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Suppliername" ServicePath="" TargetControlID="txtsupname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                
                </td>
                </tr>
                </table>
                 
                    <table id="tbl4" align="center">
                <tr >
               
                <td  >
                <asp:RadioButtonList ID="rdtrans" runat="server" CssClass="CheckBox" RepeatDirection="Horizontal"
                         AutoPostBack="True" onselectedindexchanged="rdtrans_SelectedIndexChanged">
                         <asp:ListItem>Credit</asp:ListItem>
                        <asp:ListItem>Debit</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                <asp:RadioButtonList ID="rdpay" runat="server" CssClass="CheckBox" RepeatDirection="Horizontal"
                         AutoPostBack="True" onselectedindexchanged="rdpay_SelectedIndexChanged">
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Adjust</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </tr>
                </table>
                <br />

                 <table id="Table4" align="right">
                <tr>
                <td>
                <div style="float:right; width:80px">
                <asp:Label ID="lblamt" runat="server" Text="Amount:" CssClass="Label"></asp:Label>
                <asp:Image ID="image5" runat="server" ImageUrl="~/Images/rupees.JPG" />
                </div>
                </td>
                
                <td>
                 <asp:TextBox ID="txtamt" runat="server" CssClass="TextBox" Width="80px" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtamt_TextChanged"></asp:TextBox>
                 <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                </td>
                <td>
                 <asp:Label ID="lblvou" runat="server" Text="Reference No:" CssClass="Label"></asp:Label>
                </td>
                
                <td>
                 <asp:TextBox ID="txtvou" runat="server" CssClass="TextBox" Width="80px" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtvou_TextChanged"></asp:TextBox>
                 <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                </td>
                </tr>
                <tr>
                <td>
                 <asp:Label ID="lblaccno" runat="server" Text=" Bank A/C No:" CssClass="Label"></asp:Label>
                </td>
                
                <td>
                 <asp:DropDownList ID="ddlaccno" runat="server" Width="180px"  CssClass="DropDown" AutoPostBack="true" onselectedindexchanged="ddlaccno_SelectedIndexChanged"></asp:DropDownList>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    
                <asp:Label ID="lblaccno1" runat="server" Text="lblcode"  style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</asp:Label>
                </td>
                <td>
                <div style="float:right; width:85px">
                <asp:Label ID="lblchqno" runat="server" Text="Cheque No:" CssClass="Label"></asp:Label>
                </div>
                </td>
                
                <td>
                 <asp:TextBox ID="txtchqno" runat="server" Width="80px" CssClass="TextBox" onkeypress="return alpha1(event)"></asp:TextBox>
                 <asp:Label ID="lblchqno1" runat="server" Text="lblcode"  style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</asp:Label>
                </td>
                <td>
                <asp:Label ID="lbldate1" Text="Date:" runat="server" CssClass="Label"></asp:Label>
               &nbsp;<asp:TextBox ID="txtdate1" runat="server" AutoPostBack="true"  Width="80px" 
                        ontextchanged="txtdate1_TextChanged"></asp:TextBox>
                    &nbsp;
               <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate1" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtdate1" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
                </td>
                </tr>
                <tr>
                <td align="center" class="style9">
                <div style="float:right; width:80px">
                <asp:Label ID="lblnarr" runat="server" Text="Narration:" CssClass="Label"></asp:Label>
                </div>
                </td>
                
                <td class="style9" >
                 <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Height="129px" 
                        Width="211px" onkeypress="return alpha(event)" CssClass="TextBox"></asp:TextBox>
                </td>
             
                <td class="style9">
                <br />
                <br />
                 <br />
                <br />
                 <br />
                <br />
                 <br />
                <br />
                </td>
                <td align="right" class="style9">
                <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px" 
                        CssClass="Buttons embossed-link" onclick="btnsave_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Report" Width="95px" 
                        CssClass="Buttons embossed-link" onclick="Button1_Click"/>
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  
                        CssClass="Buttons embossed-link" onclick="btnexit_Click" />
                         <asp:Button ID="Button2" runat="server" Text="Refresh" Width="95px"  
                        CssClass="Buttons embossed-link" onclick="Button2_Click"/>
                </td>
                </tr>
              </table>
              
                
           
               
    </fieldset>
    <asp:Panel ID="pdgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grcustomerdetails" align="center" runat="server" Width="829px" 
              CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
   </asp:Panel>     
</asp:Content>

