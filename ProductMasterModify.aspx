<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="ProductMasterModify.aspx.cs" Inherits="ProductMasterModify" %>
   <%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">


 <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
        
   <script type="text/javascript">
       function show(_this) {
          document.getElementById("enlarge").innerHTML = "<img src='" + _this.src + "'+'width=216 height=216' >";
           document.getElementById("Image1").innerHTML = "";
          
       }
       function Hide(_this) {
           document.getElementById("enlarge").innerHTML = "";
       }
       function move_layer(event) {
           event = event || windows.event;
           document.getElementById("enlarge").style.left = event.clientX + document.body.scrollLeft + 10 + "px";
           document.getElementById("enlarge").style.top = event.clientY + document.body.scrollTop + 10 + "px";
       }

       $(document).ready(function () {
           imageelement = $("#image1");
           imageelement.hover(
   function () {
       $(this).attr("imageelement.width", "1000px");
       $(this).attr(imageelement.height, 1000);
   }
   );
       });

//           $("[id$=txtPname]").autocomplete({
//               source: function (request, response) {
//                   $.ajax({
//                       url: '<%=ResolveUrl("~/ProductMaster.aspx/GetCustomers") %>',
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

       function cancelClick() {
           $find('modalbehavior').hide();
       }
       

       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

     


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:Panel ID="Panel1" runat="server"  > 

 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Product Master
    </h1>
    </legend>
    
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
                 <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
    </h3>

     <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
      <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
               
                </td>
                </tr>
         </table>
      <table id="Table1" align="center">
                      <tr>
                    <td>
                        <asp:Label ID="lblProd" runat="server" Text="Product Code:" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                               <asp:TextBox ID="txtProd" runat="server" width="253px"   onkeypress="return alpha1(event)" MaxLength="13" autocomplete="off" autocompletetype="None" autopostback="true"  OnTextChanged="txtProd_TextChanged"></asp:TextBox>
                               <asp:AutoCompleteExtender ID="txtcustname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Buyername10" ServicePath="" TargetControlID="txtProd" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
              </asp:AutoCompleteExtender>
                               <asp:HiddenField ID="hfCustomerId" runat="server" />
                               <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                            </td>
                             <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                               </td>
                          
                            <td>
                            <asp:Label ID="lblPname" runat="server" Text="Product Name:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                            </td>
                            <td>
                            <asp:TextBox ID="txtPname" runat="server" Width="253px" AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                    ontextchanged="txtPname_TextChanged"></asp:TextBox>
                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Buyername1" ServicePath="" TargetControlID="txtPname" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
              </asp:AutoCompleteExtender>


                                    <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                     <asp:AutoCompleteExtender ID="txtPname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtPname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                             </asp:AutoCompleteExtender>
                            </td>
                          </tr>
                          <tr >
                          <td colspan="6" align="center" class="style9">
                           <asp:Label ID="lblchkgrp" runat="server" Text="Pharmacy" CssClass="Label"></asp:Label>
                        <asp:CheckBox ID="chkgroup" runat="server"  CssClass="CheckBox" onkeydown="return(event.keyCode != 13)" autopostback="true" oncheckedchanged="chkgroup_CheckedChanged"/>
                          <asp:Label ID="lblGcode" runat="server" Text="Group Name:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                          <asp:DropDownList ID="ddgrpcode" runat="server" Width="150px"  CssClass="DropDown" AutoPostBack="true" 
                              onselectedindexchanged="ddgrpcode_SelectedIndexChanged"></asp:DropDownList>
                             <asp:Label ID="addnew" runat="server"></asp:Label>

                             
                             
                          </td>
                              <td class="style9">
                                   <asp:Label ID="lblgrcode" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                              </td>
                               <td class="style9">
                                   <asp:Label ID="lblpfflag" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                              </td>
                          </tr>
                         </table>
           </fieldset>
        </asp:Panel> 
       <table>
           <tr>
               <td>

               </td>
           </tr>
       </table>

    <asp:Panel ID="Panel2" runat="server" Width="1000px" > -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Width="1000px" > 
    <ContentTemplate>
        <fieldset class="BigFieldSet" id="medicinedetails" >
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Product Details
    </h1>
    </legend>
    <h3>
     
    </h3>
            <table>
                    

                           <tr>
                          <td>
                              <asp:Label ID="lblGecode" runat="server" Text="Generic Name:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                              </td>
                            <td>
                                <asp:DropDownList ID="ddGecode" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown"  onkeypress="return alpha(event)" OnSelectedIndexChanged="ddGecode_SelectedIndexChanged" ></asp:DropDownList>
                               
                            </td>

                               <td>
                                   <asp:Label ID="lblgncode" runat="server"></asp:Label>
                                   <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                               </td>
                          <td align="left">
                              <asp:Label ID="lblChem" runat="server" Text="Chemical Composition:" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                              </td>
                            <td>
                              <asp:DropDownList ID="ddchem" runat="server" Width="200px" AutoPostBack="true" CssClass="DropDown"  onkeypress="return alpha(event)" OnSelectedIndexChanged="ddchem_SelectedIndexChanged"></asp:DropDownList>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                              <asp:Label ID="lblcemcode" runat="server"></asp:Label>
                          </td>
                              
                                <td align="left" >
                               <asp:Label ID="lblmed" runat="server" Text="Medicine Type:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                             <asp:DropDownList ID="ddmed" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown" onkeypress="return alpha(event)" OnSelectedIndexChanged="ddmed_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>

                                <td>
                             <asp:Label ID="lblmedcode" runat="server"></asp:Label>
                          </td>
                               
                                        
                        </tr>
   

                      <tr>
                          
                         <td align="left">
                             <asp:Label ID="lblunit" runat="server" Text="Unit:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddunit" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown"  onkeypress="return alpha(event)" OnSelectedIndexChanged="ddunit_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                          <td>
                             <asp:Label ID="lblunitcode" runat="server"></asp:Label>
                          </td>
                         <td align="left">
                               <asp:Label ID="lblform" runat="server" Text="Form:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                             <asp:DropDownList ID="ddform" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown" OnSelectedIndexChanged="ddform_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                             <asp:Label ID="lblformcode" runat="server"></asp:Label>
                          </td>
                         
                         <td align="left">
                             <asp:Label ID="lblmanu" runat="server" Text="Manufacturer:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddmanu" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown"  onkeypress="return alpha(event)"  OnSelectedIndexChanged="ddmanu_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                          <td>
                              <asp:Label ID="lblmanucode" runat="server"></asp:Label>
                          </td>
                    </tr>



                    <tr>
                           <td align="left">
                               <asp:Label ID="lblpack" runat="server" Text="Pack Size:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                             <asp:DropDownList ID="ddpack" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown" onkeypress="return alpha(event)" OnSelectedIndexChanged="ddpack_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                        <td>
                             <asp:Label ID="lblPackcode" runat="server"></asp:Label>
                        </td>
                         <td align="left">
                               <asp:Label ID="Label5" runat="server" Text="Shelf :"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                             <asp:DropDownList ID="ddlshelf" runat="server" Width="150px" AutoPostBack="true"  CssClass="DropDown" onkeypress="return alpha(event)" OnSelectedIndexChanged="ddlshelf_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                              <asp:Label ID="lblShelfcode" runat="server"></asp:Label>
                          </td>
                       

                         <td align="left">
                             <asp:Label ID="Label6" runat="server" Text="Rack"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddlrow" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown" onkeypress="return alpha(event)" OnSelectedIndexChanged="ddlrow_SelectedIndexChanged"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                        <td>
                            <asp:Label ID="lblShelfcount" runat="server"></asp:Label>
                        </td>

                    </tr>
                <tr>
                     
                     <td align="left">
                             <asp:Label ID="lblsupplier" runat="server" Text="Supplier Name:"  Font-Size="Medium" Font-Bold="true" CssClass="Label" Visible="false"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddsupplier" runat="server" Width="150px" AutoPostBack="true" CssClass="DropDown" onkeypress="return alpha(event)"  OnSelectedIndexChanged="ddsupplier_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                    <td>
                        <asp:Label ID="lblsuplier" runat="server"></asp:Label>
                    </td>
                    
                    <td align="left" >
                               <asp:Label ID="Label7" runat="server" Text="Reorder level"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                              <asp:TextBox ID="txtrecordlevel" runat="server"  width="150px" OnTextChanged="txtrecordlevel_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>


                           
            
                         <td>

                         </td>

                    
                        
                </tr>
</table>
<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ddGecode"
        BackgroundCssClass="modal" PopupControlID="Panel5" Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel5" runat="server" Style="display: none;">
        <iframe id="Iframe1" src="Generic.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
         <asp:Button ID="Button1" runat="server" Text="Close"  CssClass="Buttons embossed-link" onclick="btncancel1_click" />
    </asp:Panel> 

     <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="ddchem"
        BackgroundCssClass="modal" PopupControlID="Panel6" Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel6" runat="server" Style="display: none;">
        <iframe id="Iframe2" src="Chemical.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button2" runat="server" Text="Close"  CssClass="Buttons embossed-link" onclick="btncancel2_click" />
    </asp:Panel> 

     <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="ddmed"
        BackgroundCssClass="modal" PopupControlID="Panel7" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel7" runat="server" Style="display: none;">
        <iframe id="Iframe3" src="Medicine.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
           <asp:Button ID="Button3" runat="server" Text="Close"  CssClass="Buttons embossed-link" onclick="btncancel3_click" />
    </asp:Panel> 

     <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="ddunit"
        BackgroundCssClass="modal" PopupControlID="Panel8" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel8" runat="server" Style="display: none;">
        <iframe id="Iframe4" src="Unitmaster.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
           <asp:Button ID="Button4" runat="server" Text="Close" CssClass="Buttons embossed-link" onclick="btncancel4_click" />
    </asp:Panel> 

      <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="ddform"
        BackgroundCssClass="modal" PopupControlID="Panel9" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel9" runat="server" Style="display: none;">
        <iframe id="Iframe5" src="Formmaster.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
         <asp:Button ID="Button5" runat="server" Text="Close"  CssClass="Buttons embossed-link" onclick="btncancel5_click" />
    </asp:Panel> 


      <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server" TargetControlID="ddmanu"
        BackgroundCssClass="modal" PopupControlID="Panel10" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel10" runat="server" Style="display: none;">
        <iframe id="Iframe6" src="Manufacture.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button6" runat="server" Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel6_click" />
    </asp:Panel> 

     <asp:ModalPopupExtender ID="ModalPopupExtender8" runat="server" TargetControlID="ddpack"
        BackgroundCssClass="modal" PopupControlID="Panel11" BehaviorID="modalbehavior"
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel11" runat="server" Style="display: none;">
        <iframe id="Iframe7" src="Packsize.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button7" runat="server"  Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel7_click" />
    </asp:Panel> 

     <asp:ModalPopupExtender ID="ModalPopupExtender9" runat="server" TargetControlID="ddlshelf"
        BackgroundCssClass="modal" PopupControlID="Panel12" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel12" runat="server" Style="display: none;">
        <iframe id="Iframe8" src="Shelf.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
       <asp:Button ID="Button8" runat="server"  Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel8_click" />
    </asp:Panel> 

    <asp:ModalPopupExtender ID="ModalPopupExtender10" runat="server" TargetControlID="ddlrow"
        BackgroundCssClass="modal" PopupControlID="Panel13" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel13" runat="server" Style="display: none;">
        <iframe id="Iframe9" src="Shelf.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button9" runat="server"  Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel9_click" />
    </asp:Panel> 


     <asp:ModalPopupExtender ID="ModalPopupExtender13" runat="server" TargetControlID="ddsupplier"
        BackgroundCssClass="modal" PopupControlID="Panel14" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel14" runat="server" Style="display: none;">
        <iframe id="Iframe10" src="Suppliermaster.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button10" runat="server" Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel10_click" />
    </asp:Panel> 
             </fieldset>

        </ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
     <table>
           <tr>
               <td>

               </td>
           </tr>
       </table>

       
    <asp:Panel ID="Panel3" runat="server" Width="1000px"> 
    
    <fieldset class="BigFieldSet" id="medshelf1">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Product Details
    </h1>
    </legend>
    <h3>
     
    </h3>
        <table>

                       <tr>
                           <td align="left">
                               <asp:Label ID="lblshelf" runat="server" Text="Shelf Name:"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                             <asp:DropDownList ID="ddshelf" runat="server" Width="100px"  AutoPostBack="true" CssClass="DropDown" OnSelectedIndexChanged="ddshelf_SelectedIndexChanged"   ></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                           <td>
                              <asp:Label ID="lblShelf1code" runat="server"></asp:Label>
                           </td>
                         <td align="left">
                             <asp:Label ID="lblrow" runat="server" Text="Rack"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddrow" runat="server" Width="100px" AutoPostBack="true" CssClass="DropDown" OnSelectedIndexChanged="ddrow_SelectedIndexChanged"  ></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                           <td>
                              <asp:Label ID="lblShelf1count" runat="server" Text=""></asp:Label>
                           </td>
                          <td align="left" >
                               <asp:Label ID="lblreorder" runat="server" Text="Reorder level"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                           </td>
                          <td>
                              
                              <asp:TextBox ID="txtreorder" runat="server" width="100px" OnTextChanged="txtreorder_TextChanged"   ></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>

                          

                            <td align="left">
                             <asp:Label ID="Label10" runat="server" Text="Unit"  Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                             </td>
                          <td>
                             <asp:DropDownList ID="ddlunit" runat="server" Width="100px" CssClass="DropDown" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"  ></asp:DropDownList>
                             <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                          <td>
                               <asp:Label ID="lblunitcode1" runat="server" Text=""></asp:Label>
                           </td>



                       </tr>

           



             </table>
             
        </fieldset>
        
        </asp:Panel> 
          <asp:Table runat="server" ID="tblPhoto" align="center">
          <asp:TableRow>
          <asp:TableCell>
          
                          <div style="float:right; width:78px">
                  <asp:Label ID="lblphoto" runat="server" Text="AddPhoto:" CssClass="Label"></asp:Label>
                         </div>
           
         
                       
          </asp:TableCell>
               <asp:TableCell>
            <asp:FileUpload runat="server" ID="FileUpload1" BackColor="yellow" ForeColor="red"  />
            &nbsp;&nbsp;
       <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" />
         </asp:TableCell>
         <asp:TableCell>
         <div id="enlarge" style="position: absolute; z-index:2;align:center"></div>
         <asp:Image runat="server" display="none" ID="Image1" visible="false" Height="100px" Width="100px" OnMouseover="show(this)" OnMouseOut="Hide(this)" />
         </asp:TableCell>
         </asp:TableRow>
        </asp:Table>
        &nbsp;<div align="left" class="SubmitButtons" 
        style="position:relative; left: 443px; top: 2px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" 
                    CssClass="Buttons embossed-link" OnClick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                    CssClass="Buttons embossed-link" onclick="btnExit_Click" />  
                </div>
                
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ddgrpcode"
        BackgroundCssClass="modal" PopupControlID="Panel4" Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.0" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel4" runat="server" Style="display: none;">
        <iframe id="frame1" src="Group.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="btncancel" runat="server" Text="Close"   CssClass="Buttons embossed-link" onclick="btncancel_click" />
    </asp:Panel> 

    

    <asp:ModalPopupExtender ID="ModalPopupExtender12" runat="server" TargetControlID="ddshelf"
        BackgroundCssClass="modal" PopupControlID="Panel15" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel15" runat="server" Style="display: none;">
        <iframe id="Iframe11" src="Shelf.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button11" runat="server"  Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel11_click" />
    </asp:Panel> 


     <%-- <asp:ModalPopupExtender ID="ModalPopupExtender13" runat="server" TargetControlID="ddrow"
        BackgroundCssClass="modal" PopupControlID="Panel16" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel16" runat="server" Style="display: none;">
        <iframe id="Iframe12" src="Shelf.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button12" runat="server" Text="Cancel" onclick="btncancel12_click" />
    </asp:Panel>
    --%> 

     <asp:ModalPopupExtender ID="ModalPopupExtender14" runat="server" TargetControlID="ddlunit"
        BackgroundCssClass="modal" PopupControlID="Panel16" 
        Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel17" runat="server" Style="display: none;">
        <iframe id="Iframe13" src="Unitmaster.aspx" width="900px" height="400px" scrolling="yes"
            class="AdditionalIframe"></iframe>
        <asp:Button ID="Button13" runat="server"  Text="Close"  CssClass="Buttons embossed-link"  onclick="btncancel13_click" />
    </asp:Panel> 

</asp:Content>

