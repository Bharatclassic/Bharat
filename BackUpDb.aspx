<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true"
    CodeFile="BackUpDb.aspx.cs" Inherits="BackUpDb" %>

     <asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
        
   <script type="text/javascript">
       $(function () {
           $("[id$=txtchem]").autocomplete({
               source: function (request, response) {
                   $.ajax({
                       url: '<%=ResolveUrl("~/Chemical.aspx/GetCustomers") %>',
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

       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || (k >= 37 && k <= 40));   //k=9(keycode for tab)
       }


       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

      
  
</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="BigFieldSet">
        <legend class="BigLegend">
            <h1>
                Backup DataBase
            </h1>
        </legend>

          <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>


        <ul>
            <asp:DataList ID="dlAllTables" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
                <ItemTemplate>
                    <li style="margin: 0 10px;">
                        <asp:Label ID="chkTblNames" runat="server" CssClass="Label" Text='<% #Eval("TABLE_NAME") %>' />
                    </li>
                </ItemTemplate>
            </asp:DataList>
        </ul>
        <asp:Table ID="tblColumns" runat="server">
        </asp:Table>
        <div class="SubmitButtons" style="width: 900PX;">
            <asp:Button CssClass="Buttons embossed-link" ID="btnBackUp" runat="server" Text="Backup Database"
                OnClick="btnBackUp_Click" />
       
            <asp:Button CssClass="Buttons embossed-link" ID="btnExit" Text="Exit" runat="server"
                OnClick="btnExit_Click" />
        </div>
    </fieldset>
</asp:Content>
