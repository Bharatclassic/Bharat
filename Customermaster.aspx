<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Customermaster.aspx.cs" Inherits="Customermaster" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
 <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
   <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

 <script type="text/javascript">


              $(function () {
                  $("[id$=txtstate]").autocomplete({
                      source: function (request, response) {
                          $.ajax({
                              url: '<%=ResolveUrl("~/Customermaster.aspx/Getstate") %>',
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
//                           alert(response.responseText);
                       },
                       failure: function (response) {
//                           alert(response.responseText);
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
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
       }
       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }
       function toLower(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toLowerCase();
       } 

        function alpha1(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k >= 48 && k <= 57) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
       }
       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

</script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Customer
    </h1>
    </legend>
      <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
        <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuc" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>
          <tr>
              <td>
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" 
                      CssClass="Buttons embossed-link" onclick="btn_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
              <%-- <asp:Label ID="lblmodify" runat="server" Text="Modify" Font-Bold="true" Font-Size="Large" ForeColor="Black"></asp:Label>&nbsp;&nbsp; <asp:CheckBox ID="chkmodify" runat="server" OnCheckedChanged="chkmodify_CheckedChanged"  />--%>
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="Modify" CssClass="Buttons embossed-link" OnClick="btnmodify_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />
               
            </td>
        </tr>
    </table>
      
      <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
                </h3>
        <div align="right">
           <asp:TextBox ID="txtdate" runat="server" AutoPostBack="true" 
                ontextchanged="txtdate_TextChanged" TabIndex="1" Width="80px"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtdate" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
              </div>
     
        <div align="right"> 
      
           <br />
    
       <asp:CheckBox ID="chkrural" runat="server"  Text="Rural" Font-Bold="true"  CssClass="CheckBox"
               Font-Size="Large" oncheckedchanged="chkrural_CheckedChanged" AutoPostBack="true" TabIndex="2"/>
          <asp:CheckBox ID="chkurban" runat="server"  Text="Urban" Font-Bold="true" AutoPostBack="true" CssClass="CheckBox"
               Font-Size="Large" oncheckedchanged="chkurban_CheckedChanged" TabIndex="3"/>
</div>
    <table id="Table1" align="center">
    <tr>
    <td align="right">
        <asp:Label ID="lblcustname" runat="server" Text="Customer Name" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtcustname" runat="server" width="253px" AutoPostBack="true"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"
             ontextchanged="txtcustname_TextChanged" TabIndex="4"></asp:TextBox>
              <asp:Label ID="Label1" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="left" Font-Bold="true" CssClass="Label"></asp:Label>
         <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
              <asp:AutoCompleteExtender ID="txtcustname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtcustname" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
              </asp:AutoCompleteExtender>
              
      </td>
       <td align="right">
        <asp:Label ID="Label2" runat="server" Text="Customer Code" Font-Bold="true" CssClass="Label"></asp:Label>
       </td>
       <td>
       <asp:TextBox ID="txtcustcode" runat="server" width="200px" AutoPostBack="true" ReadOnly="true" onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"
             ></asp:TextBox>
       </td>


     
      
    </tr>
     <tr>
    <td align="right">
        <asp:Label ID="lbldoorno" runat="server" Text="Door No/Village" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtdoorno" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" TabIndex="5"></asp:TextBox>
        
   <asp:Label ID="lblsdoorno" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>
        </td>
    </tr>
     <tr>
    <td align="right">
       
        <asp:Label ID="lbladd1" runat="server" Text="Address 1" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtadd1" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" TabIndex="6" >
        </asp:TextBox>
         <asp:Label ID="lblsadd1" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>
       
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="lbladd2" runat="server" Text="Address2" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtadd2" runat="server"  width="253px"  onkeyup="return toUpper(this.id)"  TabIndex="7"></asp:TextBox>
      <asp:Label ID="lblsadd2" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>  
    </td>
    </tr>
     <tr>
    <td align="right">
        <asp:Label ID="lblhobli" runat="server" Text="Hobli" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txthobli" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"  TabIndex="8"></asp:TextBox>
         <asp:Label ID="lblshobli" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>  
    </td>
     <td align="right">
        <asp:Label ID="lbltaluk" runat="server" Text="Taluk" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txttaluk" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" TabIndex="9"></asp:TextBox>
        <asp:Label ID="lblstaluk" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>  
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="lbldist" runat="server" Text="District" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtdist" runat="server"  width="253px"  onkeypress="return alpha(event)" onkeyup="return toUpper(this.id)"  TabIndex="10" ></asp:TextBox>
        <asp:Label ID="lblsdist" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>  
    </td>

    </tr>
   <tr>
    <td align="right">
        <asp:Label ID="lblcity" runat="server" Text="City" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtcity" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" TabIndex="11"></asp:TextBox>
         <asp:Label ID="lblscity" runat="server" Text="*" style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center" Font-Bold="true" CssClass="Label"></asp:Label>  
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="lblstate" runat="server" Text="State" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtstate" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"  TabIndex="12"></asp:TextBox>
        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
    </td>
     <td align="right">
        <asp:Label ID="lblcredit" runat="server" Text="Credit Limit" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td>
       <asp:TextBox ID="txtcredit" width="253px" runat="server" onkeyup="return toUpper(this.id)" onkeypress="return alpha1(event)" onkeydown="return(event.keyCode != 13)" TabIndex="13"></asp:TextBox>
       <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
   </td>
   <%-- <td align="left">
        <asp:TextBox ID="txtcredit" runat="server"  width="253px" onkeypress="return isNumberKey(event)" onkeyup="return toUpper(this.id)" onkeydown="return(event.keyCode != 13)"></asp:TextBox>
    </td>--%>
    </tr>

     <tr>
    <td align="right">
        <asp:Label ID="lblmobile" runat="server" Text="Mobile No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtmobile" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" AutoPostBack="True" OnTextChanged="txtmobilenor_TextChanged" onkeypress="return alpha1(event)" TabIndex="14" MaxLength="10"></asp:TextBox>
        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
    </td>
     <td align="right">
        <asp:Label ID="lblemail" runat="server" Text="Email" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td>
       <asp:TextBox ID="txtemail" width="253px" runat="server" onkeyup="return toLower(this.id)" onkeydown="return(event.keyCode != 13)" TabIndex="15" AutoPostBack="True" OnTextChanged="txtemail_TextChanged"></asp:TextBox>
       <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
       
    </td>
   <%-- <td align="left">
        <asp:TextBox ID="txtcredit" runat="server"  width="253px" onkeypress="return isNumberKey(event)" onkeyup="return toUpper(this.id)" onkeydown="return(event.keyCode != 13)"></asp:TextBox>
    </td>--%>
    </tr>
       <tr>
    <td align="right">
        <asp:Label ID="Label3" runat="server" Text="PAN No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtPAN" runat="server"  width="253px" TabIndex="15" 
            ontextchanged="txtPAN_TextChanged"></asp:TextBox>
    </td>
     <td align="right">
        <asp:Label ID="Label4" runat="server" Text="TIN No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtTIN" runat="server"  width="253px" TabIndex="16" 
            ontextchanged="txtTIN_TextChanged"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label5" runat="server" Text="ADHAR No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtADHAR" runat="server"  width="253px" TabIndex="17" 
            ontextchanged="txtADHAR_TextChanged"></asp:TextBox>
    </td>
     <td align="right">
        <asp:Label ID="Label6" runat="server" Text="GST No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtGST" runat="server"  width="253px" TabIndex="18" 
            ontextchanged="txtGST_TextChanged"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="lblref_code" runat="server" Text="Refered Code" Font-Bold="true" CssClass="Label"></asp:Label>
        <%-- &nbsp;<asp:Label ID="lblstar" runat="server" Text="*" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>--%>
    </td>
    <td align="left">
        <asp:DropDownList ID="ddrefcode" runat="server" Width="200px" AutoPostBack="true" onselectedindexchanged="ddrefcode_SelectedIndexChanged" TabIndex="19" >
        </asp:DropDownList>
    </td>
    <td align="right">
        <asp:Label ID="lblref_name" runat="server" Text="Refered Name" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <%--<asp:TextBox ID="txtref_name" runat="server"  width="253px"  
            onkeyup="return toUpper(this.id)" ontextchanged="txtref_name_TextChanged" 
            style="margin-bottom: 0px"></asp:TextBox>--%>
        <asp:DropDownList ID="ddrfnm" runat="server" Width="200px" AutoPostBack="true"
            onselectedindexchanged="ddrfnm_SelectedIndexChanged" TabIndex="20">
        </asp:DropDownList>
    </td>
    </tr>

    </table>

    <br />
    <br />
     <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
      <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
       <asp:GridView ID="Gridcust" runat="server"  Width="850px" CellPadding="4" BackColor="Yellow"
           AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
           GridLines="Both" OnPageIndexChanging="Gridcust_PageIndexChanging" PageSize="5">
      <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
      </asp:GridView>
      </asp:Panel></div>



 <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
      <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px" CssClass="Buttons embossed-link" onclick="btnsave_Click" TabIndex="18" />
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  CssClass="Buttons embossed-link" onclick="btnexit_Click" TabIndex="19"/>
    </div>   
      </fieldset>
</asp:Content>

