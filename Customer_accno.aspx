<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Customer_accno.aspx.cs" Inherits="Customer_accno" %>
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

       function validate(e, txt) {
           var KeyID = e.keyCode;
           if ((KeyID == 9) || (KeyID == 13)) {
               if (txt.value == '') {
                   txt.focus();
                   return false;
               }
           }
       }

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
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
            width: 85px;
        }
    </style>
    

   </asp:Content>
   <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <br />
<fieldset class="BigFieldSet">
 <legend class="BigLegend">
 <h1 style="position: relative; right: 0px;">
     Customer Account
    </h1>
    </legend>
    

     <table>
            <tr>
    <td>
     <asp:Label ID="lbldate" Text="Date :" runat="server" CssClass="Label"></asp:Label>
    </td>
    <td>
       <asp:TextBox ID="txtdate" runat="server" AutoPostBack="true" Width="80px"></asp:TextBox>
       
    </td>
    </tr>
    </table>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
                </h3>

 <div id="Div1" runat="server" style="float: left; width: 200px;  border: 1px solid gray; height: auto;">
      <table id="Table1">

       <tr>
    <td>
       <asp:Label ID="Label6" Text="Mob No:" runat="server" CssClass="Label"></asp:Label>
       <asp:Label ID="lblpanid" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>


    <tr>
    <td>
       <asp:Label ID="Label8" Text="Emailid:" runat="server" CssClass="Label"></asp:Label>
       <asp:Label ID="lbltnnor" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>

     <tr>
    <td>
       <asp:Label ID="Label7" Text="Taluk:" runat="server" CssClass="Label"></asp:Label>
       <asp:Label ID="lbltaluk" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>

   <tr>
    <td>
       <asp:Label ID="Label9" Text="Hoobli:" runat="server" CssClass="Label"></asp:Label>
       <asp:Label ID="lblhobli" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>



    </table>
</div>

                 <div id="GRN" runat="server" style="float: right; width: 250px;  border: 1px solid gray; height: auto;">
       
       
    <table id="tbl1">

      <tr>
    <td>
       <asp:Label ID="Label4" Text="Credit Limit:" runat="server" CssClass="Label"></asp:Label>
        <asp:Image ID="image3" runat="server" ImageUrl="~/Images/rupees.JPG" />
       <asp:Label ID="lblcreditlimit" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>


    <tr>
    <td>
       <asp:Label ID="Label5" Text="Credit Used:" runat="server" CssClass="Label"></asp:Label>
        <asp:Image ID="image1" runat="server" ImageUrl="~/Images/rupees.JPG" />
       <asp:Label ID="lblcredituseid" Text="" runat="server" CssClass="Label"></asp:Label>
    </td>
   </tr>



   
    <tr>
     <td>
       <asp:Label ID="Label1" Text="Advanced Amount:" runat="server" CssClass="Label"></asp:Label>
        <asp:Image ID="image2" runat="server" ImageUrl="~/Images/rupees.JPG" />
        <asp:Label ID="txtcredit" runat="server" CssClass="Label"></asp:Label>
     </td>
   

    
    </tr>

     <tr>
    <td>
       <asp:Label ID="lblbal" Text=" Total Balance:" runat="server" CssClass="Label"></asp:Label>
        <asp:Image ID="image4" runat="server" ImageUrl="~/Images/rupees.JPG" />
       <asp:Label ID="lblbalance" Text="" runat="server" CssClass="Label"></asp:Label>
        <asp:Label runat="server" ID="txtbal" CssClass="Label"></asp:Label>
    </td>
    
    <!--<td>
    <asp:Label ID="lblCB" runat="server" CssClass="Label" Text="Credit Balance"></asp:Label>
     </td>
    <td>
     <asp:Label ID="lblAB" runat="server" CssClass="Label" Text="Advance Balance"></asp:Label>
     </td>-->
    </tr>
        </table>
    </div>
               
  

   <table id="tbl2" align="center">
   <tr>
   <td>
   <br />
    <br />
    <br />
    <br />
    </td>
     <td>
       <asp:Label ID="lblcustcode" Text="Customer Code:" runat="server" CssClass="Label"></asp:Label>
    </td>
     <td>
         <asp:TextBox ID="txtcustcode" runat="server" CssClass="TextBox"  AutoPostBack="true" width="80px" onkeypress="return alpha1(event)"
             ontextchanged="txtcustcode_TextChanged"></asp:TextBox>
          <asp:AutoCompleteExtender ID="txtcustcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
            Enabled="True" ServiceMethod="Customercode" ServicePath="" TargetControlID="txtcustcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
            UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
          </asp:AutoCompleteExtender>
     </td>
     <td> 
     <asp:Label ID="lblcustnm" Text="Customer Name:" runat="server" CssClass="Label"></asp:Label>
     </td>
     <td>
      <asp:TextBox ID="txtcustnm" runat="server" CssClass="TextBox"  width="200px" AutoPostBack="true" onkeypress="return alpha(event)" onkeyup="return toUpper(this.id)"
             ontextchanged="txtcustnm_TextChanged"></asp:TextBox>
       <asp:AutoCompleteExtender ID="txtcustnm_AutoCompleteExtender1" runat="server" DelimiterCharacters=""
            Enabled="True" ServiceMethod="Customername" ServicePath="" TargetControlID="txtcustnm" CompletionListItemCssClass="OtherCompletionItemCssClass"
            UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
        </asp:AutoCompleteExtender>
     </td>
    </tr>
   </table>
   <table id="tbl3" runat="server" align="center">
   <tr> 
   <td>
     <asp:Label ID="Label2" Text="Transaction Type:" runat="server" CssClass="Label"></asp:Label>
   </td>
    <td>
     <asp:RadioButtonList ID="rdtrans" runat="server" AutoPostBack="true" CssClass="CheckBox"  RepeatDirection="Horizontal"  onselectedindexchanged="rdtrans_SelectedIndexChanged">
      <asp:ListItem Value="0">Credit</asp:ListItem>
      <asp:ListItem Value="1">Debit</asp:ListItem>
     </asp:RadioButtonList>
   </td>
    <td>
     <asp:Label ID="Label3" Text="Payment Type:" runat="server" CssClass="Label"></asp:Label>
     </td>
     <td>
      <asp:RadioButtonList ID="rdpay" runat="server"  CssClass="CheckBox"  AutoPostBack="true"
            RepeatDirection="Horizontal"
            onselectedindexchanged="rdpay_SelectedIndexChanged">
         <asp:ListItem>Cash</asp:ListItem>
         <asp:ListItem>Adjust</asp:ListItem>
     </asp:RadioButtonList>
   </td>

   
   </tr>
   </table>

  
  
  <table id="tbl4" align="center">
    
    <tr>
    <td>
    <div style="float:right; width:240px">
    <asp:Label ID="lblamt" runat="server" Text="Amount:" CssClass="Label"></asp:Label>
    <del style="font-size: 20px;">&#2352</del>

    <asp:TextBox ID="txtamt" runat="server" CssClass="TextBox" width="80px" AutoPostBack="true" onkeypress="return alpha1(event)" onkeydown="return validate(event,this);" 
    ontextchanged="txtamt_TextChanged" OnBlur="_doPostBack('txtamt','OnBlur');"></asp:TextBox>
    </div>

    </td>
    <td>
    <div style="float:left; width:240px">
    <asp:Label ID="lblvou" runat="server" Text="Receipt No         :" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtvou" runat="server" CssClass="TextBox" width="80px" ></asp:TextBox>
    </div>
    </td>
    </tr>
    
    <tr>
    <td>
    </td>
    
    <td>
    
    <div style="float:left; width:300px">
    <asp:Label ID="lblreference" runat="server" Text="Voucher No:" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtreference" runat="server" CssClass="TextBox" width="80px" 
                 onkeypress="return alpha1(event)"></asp:TextBox>
                 </div>
    </td>
    
    </tr>
    <tr>
    <td>
    <div style="float:right; width:290px">
    <asp:Label ID="lblaccno" runat="server" Text="Bank A/C Name:" CssClass="Label"></asp:Label>
    <asp:DropDownList ID="ddlaccno" runat="server" Width="150px"  CssClass="DropDown" 
            AutoPostBack="true" onselectedindexchanged="ddlaccno_SelectedIndexChanged" ></asp:DropDownList>
    </div>
    
    </td>
    
    
    
    <td>
    <div style="float:left; width:200px">
    <asp:Label ID="lblchqno" runat="server" Text="Cheque No          :" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtchqno" runat="server" CssClass="TextBox" width="80px" autocomplete="off"  onkeypress="return alpha1(event)" ontextchanged="txtchqno_TextChanged" ></asp:TextBox>    
    </div>
    </td>
    
    </tr>
    <tr>
    <td>
    <div style="float:right; width:350px">
    <asp:Label ID="lblaccno1" runat="server" Text="Bank A/C Name:" CssClass="Label"></asp:Label>
    <asp:TextBox ID="txtaccno" runat="server" CssClass="TextBox"></asp:TextBox>
    </div>
    </td>
    <td>
    <div style="float:right; width:350px">
    <asp:Label ID="lblbankaccount" Text="Bank Account:" runat="server" CssClass="Label"></asp:Label>
    <asp:DropDownList ID="dddepbankacc" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown"  onkeypress="return alpha(event)" onselectedindexchanged="dddepbankacc_SelectedIndexChanged" ></asp:DropDownList>
    </div>
    </td>
    </tr>
    <tr>
    <td>
    <div style="float:right; width:200px">
    <asp:Label ID="lbldate1" Text="Date:" runat="server" CssClass="Label"></asp:Label>
    <asp:TextBox ID="txtdate1" runat="server" AutoPostBack="true" Width="80px" 
                    ontextchanged="txtdate1_TextChanged" OnBlur="_doPostBack('txtdate1','OnBlur');"></asp:TextBox>
                    &nbsp;
                     <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate1" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtdate1" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
                                                </div>
    </td>

    <td>
    <div style="float:left; width:210px">
    <asp:Label ID="lblnarr" runat="server" Text="Narration:" CssClass="Label"></asp:Label>
    <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Height="107px" 
            width="135px" CssClass="TextBox"></asp:TextBox>
    </div>
    </td>
    </tr>
    <tr>
    <td>
    <asp:Panel ID="PanelInvc" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
    <table align="center">
    <tr>
    <td>
    <asp:RadioButtonList AutoPostBack="true" ID="rdList" CssClass="RadioButton" CellPadding="0" CellSpacing="0"
            runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="142px" TabIndex="1"
           Font-Size="Medium" 
            onselectedindexchanged="rdList_SelectedIndexChanged">
    <asp:ListItem Text="All" Value="all" Selected="True"></asp:ListItem>
    <asp:ListItem Text="InvoiceWise" Value="invc"></asp:ListItem>
    </asp:RadioButtonList>
    </td>
    <td>
    <asp:Label ID="lblInvo" runat="server" Visible="false" Text="Select Invoice :" CssClass="Label"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="ddlInvo" Visible="false" runat="server" CssClass="DropDown" 
            AutoPostBack="true" TabIndex="2" 
            onselectedindexchanged="ddlInvo_SelectedIndexChanged"></asp:DropDownList>
    </td>
            <caption>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <tr>
                    <td>
                        <asp:Button ID="btnPrnt" runat="server" CssClass="Buttons embossed-link" 
                            onclick="btnPrnt_Click" TabIndex="3" Text="Get Report" />
                    </td>
                </tr>
        </caption>
    </tr>
    </table>
    </asp:Panel>
    </td>
    </tr>
    <tr>
    <td align="left">
    <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px" 
             CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
             <asp:Button ID="report" runat="server" 
                    Text="Report" Width="95px" 
             CssClass="Buttons embossed-link" onclick="report_Click"/>
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  
                        CssClass="Buttons embossed-link" onclick="btnexit_Click" />
      <asp:Button ID="Button1" runat="server" Text="Refresh" Width="95px"  
                        CssClass="Buttons embossed-link" onclick="Button1_Click" />
    </td>
    </tr> 
    </table>
    <table id="tbl12" align="center">
    <tr>
    <td>
    
    <asp:Panel ID="pdgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grcustomerdetails" align="center" runat="server" Width="829px" 
              CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found" 
            onselectedindexchanged="grcustomerdetails_SelectedIndexChanged">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
   </asp:Panel> 
   </td>
    </tr>
   </table>



     </fieldset>

</asp:Content>

