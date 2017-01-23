<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Shelf.aspx.cs" Inherits="Self" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
//        $(function () {
//            $("[id$=txtself]").autocomplete({
//                source: function (request, response) {
//                    $.ajax({
//                        url: '<%=ResolveUrl("~/Shelf.aspx/GetCustomers") %>',
//                       data: "{ 'prefix': '" + request.term + "'}",
//                       dataType: "json",
//                       type: "POST",
//                       contentType: "application/json; charset=utf-8",
//                       success: function (data) {
//                           response($.map(data.d, function (item) {
//                               return {
//                                   label: item.split('-')[0],
//                                   val: item.split('-')[1]
//                               }
//                           }))
//                       },
//                       error: function (response) {
//                           alert(response.responseText);
//                       },
//                       failure: function (response) {
//                           alert(response.responseText);
//                       }
//                   });
//               },
//               select: function (e, i) {
//                   $("[id$=hfCustomerId]").val(i.item.val);
//               },
//               minLength: 1
//           });
//       });

       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };
       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 48 && k < 57) || (k > 96 && k < 123) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
       }
       function alpha1(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k >= 48 && k <= 57) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
       }
       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }
 
</script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Shelf
    </h1>
    </legend>
      <table id="Table3" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
      <tr>
           <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" 
                    CssClass="Buttons embossed-link" onclick="btn_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
              <%-- <asp:Label ID="lblmodify" runat="server" Text="Modify" Font-Bold="true" Font-Size="Large" ForeColor="Black"></asp:Label>&nbsp;&nbsp; <asp:CheckBox ID="chkmodify" runat="server" OnCheckedChanged="chkmodify_CheckedChanged"  />--%>
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="Modify" 
                    CssClass="Buttons embossed-link" onclick="btnmodify_Click" style="height: 26px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />
               
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlTextBoxes" runat="server">
    </asp:Panel>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
    <table id="table2" align="center" runat="server"   cellspacing="5">
    <tr>
                    <td align="right" class="auto-style1">
                        <asp:Label ID="lblself" runat="server" Text="Shelf:"  CssClass="Label"></asp:Label>
                     </td>

                        <td align="left">
                            <asp:TextBox ID="txtself" width="253px" runat="server" MaxLength="3" CssClass="TextBox"
                               onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" ontextchanged="txtself_TextChanged" AutoPostBack="true" onkeydown="return(event.keyCode != 13)" TabIndex="1"></asp:TextBox>
                               <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                             <asp:AutoCompleteExtender ID="txtself_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtself" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                             <%--<asp:HiddenField ID="hfCustomerId" runat="server" />--%>
                        </td>
                        <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                               </td>

                      </tr>

                     

                      </table>
           <asp:Panel ID="Panel2" runat="server">
           <table id="Table1" align="center" runat="server"   cellspacing="5">
                    <tr> 
                    <td align="right" class="auto-style1">
                        <asp:Label ID="lblrows" runat="server" Text="Rack:" CssClass="Label"></asp:Label>
                     </td>

                        <td align="left">
                            <asp:TextBox ID="txtrows" width="253px" runat="server" AutoPostBack="true" MaxLength="2" ontextchanged="txtrows_TextChanged" onkeypress="return alpha1(event)" onkeydown="return(event.keyCode != 13)" TabIndex="2" CssClass="TextBox"></asp:TextBox>
                            <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                             
                        </td>

                     </tr> 
              </table>

               </asp:Panel>

          
        <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
          
        
        <asp:UpdatePanel ID="updateGrid"  runat="server" UpdateMode="Conditional"> 
        <ContentTemplate>
         <asp:GridView ID="Gridviewshelf" runat="server" Width="629px" CellPadding="4" BackColor="Yellow"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both" OnPageIndexChanging="Gridviewshelf_PageIndexChanging" PageSize="5">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         </asp:GridView>
           </ContentTemplate>
         </asp:UpdatePanel> </div>
         <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
          <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px" CssClass="Buttons embossed-link" OnClick="btnsave_Click" TabIndex="3"/>
          <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" OnClick="btnexit_Click" TabIndex="4"/>         
                </div>   
     </fieldset>
</asp:Content>




