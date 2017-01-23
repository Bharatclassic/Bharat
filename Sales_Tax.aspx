<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Sales_Tax.aspx.cs" Inherits="Sales_Tax" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
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
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
   <h1 style="position: relative; right: 0px;">
     Sales Tax
    </h1>
    </legend>
     <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
      <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" 
                    CssClass="Buttons embossed-link" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="Modify" 
                    CssClass="Buttons embossed-link"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />
               
            </td>
        </tr>
    </table>
     <table id="tbl" align="right">
    <tr>
    <td>
     <asp:Label ID="lbldate" Text=" Effective Date :" runat="server" CssClass="Label"></asp:Label>
    </td>
    <td>
       <asp:TextBox ID="txtdate" runat="server" AutoPostBack="true" Width="80px" TabIndex="1" ontextchanged="txtdate_TextChanged"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtdate" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
       
    </td>
    </tr>
    </table>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
           
             
             <table id="Table1" align="center" runat="server"  cellspacing="5">
                      <tr>
                    <td align="left">
                       <asp:Label ID="lblgrpnm" runat="server" Text="Select Group Master:" CssClass="Label"></asp:Label>
                        <span style="color: Red; font: bold 12px 0 'Segoe Ui';" align="center">*</span>
                    </td>
                   
                
                          <td>

                             <asp:DropDownList ID="ddlsalestax" runat="server" Width="150px" TabIndex="2" CssClass="DropDown" AutoPostBack="true" onselectedindexchanged="ddlsalestax_SelectedIndexChanged"></asp:DropDownList>   
                                 <asp:HiddenField ID="hfCustomerId" runat="server" />
                                 </td>
                                 <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                               </td>

                                 <td>
                                   <asp:Label ID="lblcode1" runat="server" Text="lblcode"></asp:Label>
                               </td>

                          
                          
                      </tr>
                <tr>
                 <td align="left">
                       <asp:Label ID="Label1" runat="server" Text="Rate Of Tax:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                         <asp:TextBox ID="txtrateoftax" runat="server" TabIndex="3"  CssClass="TextBox" Width="145px" onkeyup="return toUpper(this.id)" ></asp:TextBox> 
                         <asp:Label ID="Label2" runat="server" Text="Rate Of Tax:" CssClass="Label">%</asp:Label> 
                    </td></tr>
                           
    </table>
            
              
              
       
           <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;">  
                <asp:Button ID="Button1" runat="server" Text="Save" TabIndex="4" Width="95px" OnClick="Button1_Click1" CssClass="Buttons embossed-link"/>
               <asp:Button ID="Button2" runat="server" OnClick="Button4_Click" Text="Exit" Width="90px" CssClass="Buttons embossed-link" TabIndex="5"/>
            </div>
            </fieldset>
</asp:Content>

