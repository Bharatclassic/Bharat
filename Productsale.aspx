<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Productsale.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server"> 
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
<style>
.highlight
{
  color:red;
  font-weight:bold;
}
</style>
<script type="text/javascript">
 function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
       }
       function alpha1(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k >= 48 && k <= 57) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
       }

       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }

       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblinvoicenor.ClientID %>").style.display = "none";
               document.getElementById("<%=txtinvoicenor.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

 </script>

  
      
   </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   

<asp:Panel ID="Panel1" runat="server" Width="1000px" >
 

 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Sale
    </h1>
    </legend>

     <asp:UpdatePanel ID="UpdatePanel3" 
                 UpdateMode="Conditional"
                 runat="server">
                 <ContentTemplate>
    
    
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>

     


    <div align="right">
        <asp:TextBox ID="txtdate" runat="server" Width="90px" AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                    ontextchanged="txtinvoicedate_TextChanged"></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtdate" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>


    
    </div>

   
     
      <table id="Table1" align="center">

      

      <tr>

      <td>
      </td>
      
      
      </tr>
      

                      <tr>
                    <td>
                        <asp:Label ID="lblProd" runat="server" Text="Doctor Name:" Font-Size="small" Font-Bold="true" CssClass="Label"></asp:Label>

                        </td>
                        
                           <td align="left">
                               <asp:TextBox ID="txtdoctorname" runat="server"  AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                   ontextchanged="txtdoctorname_TextChanged"></asp:TextBox>
                                   <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                     <asp:AutoCompleteExtender ID="txtdoctorname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername3" ServicePath="" TargetControlID="txtdoctorname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                             </asp:AutoCompleteExtender>
                               
                            </td>

                           <td></td>
                             <td></td>
                             
                            
                          
                            <td>
                            <asp:Label ID="lblPname" runat="server" Text="Patient Name:" Font-Size="small" Font-Bold="true" CssClass="Label" ></asp:Label>

                            </td>
                            <td>
                            <asp:TextBox ID="txtpatientname" runat="server" Width="150px" AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                    ontextchanged="txtpatientname_TextChanged"></asp:TextBox>
                                    <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                      
                                     
                            </td>
                            
                            
                          </tr>
                          
                                                    <tr>


                                                    

                                   <td></td>
                             <td></td>

                                   <td>
                                                       <asp:Label ID="lblinvoicenor" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Invoice No"></asp:Label>
                                                   </td>
                                                   <td>
                                                      <asp:TextBox ID="txtinvoicenor" runat="server" Width="150px" AutoPostBack="true" ReadOnly="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"></asp:TextBox>
                                  </td>





                                                   </tr>

                         </table>
                         <div>

                          <div align="right">
                          <asp:Label ID="Label1" runat="server" Text="Stock in Hand:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                           <asp:TextBox ID="txtstock" runat="server" Width="70px" AutoPostBack="true" ReadOnly="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"></asp:TextBox>
                                    </div>





 

        <table width="800px" align="center">

            <tr>

                <td colspan="2" align="center"><b>Sale Details</b></td>

            </tr>         

            <tr>

            <td colspan="2">
           <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
         
            
      <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" 
                    HeaderStyle-BackColor="Green" Width="629px"
            AutoGenerateColumns="false" HorizontalAlign="Left"
                    OnRowDataBound="Gridview1_OnRowDataBound"
                    OnRowDeleting="Gridview1_RowDeleting">
                     <HeaderStyle HorizontalAlign="Right" /> 
                    
            <Columns>
            
            <asp:BoundField DataField="RowNumber" HeaderText="Sl.No" />
            <asp:CommandField ShowDeleteButton="true" />
            <asp:TemplateField  HeaderText="Product Code">

                <ItemTemplate>
                 <asp:TextBox ID="txtproductcode"  style="text-align:Right" Width="75px" runat="server" CssClass="TextBox"  AutoPostBack="true" OnTextChanged="txtproductcode_TextChanged"></asp:TextBox>
                 <asp:AutoCompleteExtender ID="txtproductcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername1" ServicePath="" TargetControlID="txtproductcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                 </asp:AutoCompleteExtender>
              </ItemTemplate>
               <ItemStyle HorizontalAlign="Right">
               </ItemStyle>
              
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Product Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtproductname"  style="text-align:Right"  Width="75px" runat="server" CssClass="TextBox"  AutoPostBack="true" OnTextChanged="txtproductname_TextChanged"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="txtproductname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername2" ServicePath="" TargetControlID="txtproductname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
           </asp:AutoCompleteExtender>
           </ItemTemplate>
            </asp:TemplateField>


             <asp:TemplateField HeaderText="Expiry Date">
                <ItemTemplate>
                    <asp:TextBox ID="txtexpiredate" style="text-align:Right" CssClass="TextBox" runat="server" AutoPostBack="true"  Width="75px"></asp:TextBox>
                </ItemTemplate>
               </asp:TemplateField>

                <asp:TemplateField  HeaderText="Batch No.">
                <ItemTemplate>
                   <asp:DropDownList ID="ddl_Batch" Width="75px" runat="server" CssClass="TextBox" AutoPostBack="true" onselectedindexchanged="ddl_Batch_SelectedIndexChanged" height="25px" ></asp:DropDownList> 
                     
                </ItemTemplate>
            </asp:TemplateField>


            

          <asp:TemplateField HeaderText="Stock">
                <ItemTemplate>
                    <asp:TextBox ID="txtStockinhand" Enabled="false" CssClass="TextBox" style="text-align:Right"  runat="server" AutoPostBack="true"  Width="75px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

             

            <asp:TemplateField  HeaderText="Rate" >
                <ItemTemplate>
                    <asp:TextBox ID="txtrate" ReadOnly="true" Width="50px" CssClass="TextBox" style="text-align:Right" class="txtMarks" AutoPostBack="true" Enabled="false" runat="server"></asp:TextBox>
                    
                </ItemTemplate>
            </asp:TemplateField>

          
           
            <asp:TemplateField  HeaderText="Tax%">
                <ItemTemplate>
                   
                     <asp:TextBox ID="txttax" style="text-align:Right" CssClass="TextBox" AutoPostBack="true" Enabled="false" OnTextChanged="txttax_TextChanged" Width="50px" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>


            <asp:TemplateField  HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txtquantity" style="text-align:Right" CssClass="TextBox" AutoPostBack="true" OnTextChanged="txtquantity_TextChanged" Width="50px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField  HeaderText="Discount%">
                <ItemTemplate>
                     <asp:TextBox ID="txtdiscount" style="text-align:Right" CssClass="TextBox" Width="50px" runat="server" AutoPostBack="true"  OnTextChanged="txtdiscount_TextChanged"></asp:TextBox>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField  HeaderText="Tax Rate">
                <ItemTemplate>
                    <asp:TextBox ID="txttaxrate" style="text-align:Right" CssClass="TextBox" AutoPostBack="true" enabled="false" Width="75px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField visible='false' HeaderText="Purchase Amount">
                <ItemTemplate>
                     <asp:TextBox ID="txtpurchamount" style="text-align:Right" CssClass="TextBox" visible='false'  Width="75px" runat="server" ></asp:TextBox>
                </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField visible='false' HeaderText="Tax Amount">
                <ItemTemplate>
                     <asp:TextBox ID="txttaxamount" style="text-align:Right"  visible='false'  Width="75px" runat="server" ></asp:TextBox>
                </ItemTemplate>
             </asp:TemplateField>



                
               
            

               <asp:TemplateField  HeaderText="Product Amount">
                <ItemTemplate>
                     <asp:TextBox ID="txtproamount" style="text-align:Right" CssClass="TextBox" enabled="false" Width="75px" runat="server" ></asp:TextBox>
                </ItemTemplate>
                
                 </asp:TemplateField>


                   <asp:TemplateField  HeaderText="Group Name">
                <ItemTemplate>
                     <asp:TextBox ID="txtgroupname" style="text-align:Right" CssClass="TextBox" enabled="false" Width="75px" runat="server" ></asp:TextBox>
                </ItemTemplate>
                
                 </asp:TemplateField>



                 

               
                   


                <asp:TemplateField HeaderText="Add New">
                 <ItemTemplate>
               
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add"  onclick="ButtonAdd_Click"/>
                 
                </ItemTemplate>
                 </asp:TemplateField>

                
            
                
            </Columns>
        </asp:gridview>

        </asp:Panel>

       

      


        <div style="margin-right:250px">

         <asp:Label ID="lblGcode" runat="server" Text="Payment Type:" Font-Size="small" Font-Bold="true" CssClass="Label" ></asp:Label>
                          <asp:DropDownList ID="ddpaymenttype" runat="server" height="25px"  
                              CssClass="DropDown" AutoPostBack="true" onkeyup="return validate(this.id)"
                              onselectedindexchanged="ddpaymenttype_SelectedIndexChanged">
                           
                          </asp:DropDownList>
       </div>

       <asp:Panel ID="Panel3" runat="server"> 

        <fieldset class="Address">
            <legend>
                <h3>
                    Card Details</h3>
            </legend>
            <table align="center" cellpadding="3px">
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Size="small" Text="Card Type" CssClass="Label"></asp:Label>
                           </td>
                           <td>
                  
                         <asp:DropDownList ID="ddlpaytype" runat="server" width="100px"  
                              CssClass="DropDown" AutoPostBack="true" 
                             onkeyup="return validate(this.id)" 
                             onselectedindexchanged="ddlpaytype_SelectedIndexChanged">
                           </asp:DropDownList>

                           </td>
                       
                 

                     <td>
                        <asp:Label ID="lblbillno" runat="server" Font-Size="small" Text="Bill No" CssClass="Label"></asp:Label>
                    </td>
                   
                    <td>
                         <asp:Label ID="lblvbillno" style="text-align:Right" CssClass="Label" runat="server" ReadOnly="true"></asp:Label>
                    </td>
                     </tr>
                     <tr>


                    <td>
                        <asp:Label ID="lbltrcrno" Font-Size="small" runat="server" Width="190px" Text="Card No :xxxx-xxxx-xxxx-" CssClass="Label"></asp:Label>
                    </td>
                    
                    
                    <td>
                       
                        <asp:TextBox ID="txtcardno" style="text-align:left" Width="100px" onkeypress="return alpha1(event)" onkeydown="return(event.keyCode != 13)"  MaxLength="4" runat="server" CssClass="TextBox" ></asp:TextBox>
                       
                    </td>

                     <td>
                        <asp:Label ID="lblAmount" Font-Size="small" runat="server" Text="Amount" CssClass="Label"></asp:Label>
                    </td>
                    
                    <td>
                        <asp:TextBox ID="txtcramount"  Enabled="false" Width="150px"  style="text-align:Right" runat="server" CssClass="TextBox"></asp:TextBox>
                    </td>
                  </tr>
                 
                <tr>
                   <td>
                        <asp:Label ID="Label8" runat="server" Font-Size="small" Text="Trans No" CssClass="Label"></asp:Label>
                    </td>
                  
                    <td>
                        <asp:TextBox ID="txttransno" Width="100px" style="text-align:Right" 
                            runat="server" autopostback="true" ontextchanged="txttransno_TextChanged"  CssClass="TextBox"></asp:TextBox>
                    </td>
                   
                </tr>

                



            </table>
        </fieldset>
    </asp:Panel> 

     <asp:Panel ID="Panel4"  runat="server"> 

        <fieldset class="Address">
            <legend>
                <h3>
                    Credit Customer</h3>
            </legend>
            <table align="center" cellpadding="3px">
                <tr>
                <td>
                   <asp:Label ID="lblbalamt" runat="server" Font-Size="small" Text="Balance Amount:" CssClass="Label"></asp:Label>
                   </td>
                   <td>
                     <del style="font-size: 20px;">&#2352</del>
                   <asp:Label ID="txtbal" runat="server" style="text-align:Right" CssClass="Label"></asp:Label>
               </td>
               
                  </tr>
     
     
            <tr>
                    <td>
                        <asp:Label ID="Label3" Font-Size="small" runat="server" Text="Customer Code" CssClass="Label"></asp:Label>
                    </td>
                    
                    <td>
                        <asp:TextBox ID="txtcustomercode" runat="server" style="text-align:Right" CssClass="AddressTextBox" AutoPostBack="True" ontextchanged="txtcustomercode_TextChanged"></asp:TextBox>
                         <asp:AutoCompleteExtender ID="txtsupcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Customercode" ServicePath="" TargetControlID="txtcustomercode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                       
                    </td>
                 
                    <td>
                        <asp:Label ID="Label4" Font-Size="small" runat="server" Text="Customer Name" CssClass="Label"></asp:Label>
                    </td>
                   
                    <td>
                        <asp:TextBox ID="txtcustname" runat="server" style="text-align:Right" CssClass="AddressTextBox" AutoPostBack="True"  ontextchanged="txtcustname_TextChanged"></asp:TextBox>
                         <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Customername" ServicePath="" TargetControlID="txtcustname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                    </td>
               </tr>
               <tr>
                    <td>
                        <asp:Label ID="Label10" Font-Size="small" runat="server" Text="Bill No" CssClass="Label"></asp:Label>
                    </td>
                   
                    <td>
                        <asp:Label ID="lblbillnor" style="text-align:Right" CssClass="Label" runat="server" ReadOnly="true"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="lblamount1" Font-Size="small" runat="server" Text="Amount" CssClass="Label"></asp:Label>
                    </td>
                   
                    <td>
                        <asp:TextBox ID="txtamount" ReadOnly="true" style="text-align:Right"  CssClass="AddressTextBox" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel> 

       
           
            <table align="right">
             <tr>
                <td>
                  <asp:Label ID="Label5" runat="server" Text="Total Product Amount:" Font-Size="small" Font-Bold="true" CssClass="Label" ></asp:Label>
                </td>
                <td>
                 <del style="font-size: 20px;">&#2352</del>
                   <asp:TextBox ID="txtpramount" style="text-align:Right" runat="server" Width="100px" CssClass="TextBox"
                        AutoPostBack="true" Enabled="false" ></asp:TextBox>
                 
                </td>
             </tr>

                  <tr>
                  <td>
                  
                  <asp:Label ID="Label11" runat="server" Text="Total Discount Amount:"  Font-Size="small"  Font-Bold="true" CssClass="Label" ></asp:Label>
                   </td>
                   <td>
                    <del style="font-size: 20px;">&#2352</del>
                   <asp:TextBox ID="txtdiscount" Width="100px" style="text-align:Right" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                  
                  </td>
                 

                  </tr>


                  <tr>
                  <td>

                  <asp:Label ID="Label2" runat="server" Text="Total Tax Amount:"   Font-Size="small"  Font-Bold="true" CssClass="Label" ></asp:Label>
                   </td>
                   <td>
                    <del style="font-size: 20px;">&#2352</del>
                   <asp:TextBox ID="txttax" Width="100px" style="text-align:Right" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                  
                  
                  </td>
                 

                  </tr>

                  <tr>
                  <td>
                 <asp:Label ID="Label7" runat="server" Text=" Total Final Amount:"  Font-Size="small"  CssClass="Label"  Font-Bold="true"></asp:Label>
                 </td>
                 <td>
                  <del style="font-size: 20px;">&#2352</del>
                   <asp:TextBox ID="txttotalamount"  Width="100px" style="text-align:Right" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                  
                 
                  </td>

                  </tr>
                
            </table>
        
        
</div>

</td>

 



            </tr>

        </table>
        

    </div>

    <asp:Panel ID="pdgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grprodsaledetails" align="center" runat="server" Width="829px" CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
   </asp:Panel> 


    <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grprodsaledetails1" align="center" runat="server" Width="829px" CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
<asp:Label ID="lblcardamount" runat="server" Text=""  Font-Size="small"  Font-Bold="true" CssClass="Label" ></asp:Label>
   </asp:Panel> 
         </ContentTemplate>
</asp:UpdatePanel>  

         
              <div align="left" class="SubmitButtons" style="position:relative; left: 300px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" TabIndex="2" CssClass="Buttons embossed-link" OnClick="Button1_Click"/>
             <asp:Button ID="btnprint" runat="server" Text="Print"  Width="95px" TabIndex="3" CssClass="Buttons embossed-link" OnClick="btnprint_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" OnClick="btnExit_Click" TabIndex="3"/>  
                </div>
                
            

           </fieldset>

            <asp:HiddenField ID="HiddenField" runat="server" />
            <asp:ModalPopupExtender ID="mpeMessagePopup" runat="server" PopupControlID="pnlMessageBox"
               TargetControlID="HiddenField" 
                BackgroundCssClass="modal">
               
            </asp:ModalPopupExtender>
            <asp:UpdatePanel runat="server" ID="pnlMessageBox" Style="display: none;">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnMessagePopupTargetButton" />
            </Triggers>
            <ContentTemplate>
                <div align="center" class="ConfirmBoxHeader">
                    <asp:Label ID="lblMessagePopupHeading" Text="Information" 
                        runat="server" CssClass="Label" align="center"></asp:Label>
               </div>
               <div class="Clear">
        </div>
                <div class="ConfirmBox">
            <div align="center" style="width: 380px; height: 62px; margin: 0 auto;">
                <div style="float: left;">
                    <img src="Images/1389184475_Error.png" alt="" />
                </div>
                <div style="float: left; width: 300px; text-align: center" align="center">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="Label"  ForeColor="Red" align="center"></asp:Label>
                </div>
            </div>
            <div class="Clear">
            </div>
            <div align="center">
                <asp:Button ID="btnMessagePopupTargetButton" runat="server" Text="OK" CssClass="Buttons embossed-link"
                  OnClick="btnMessagePopupTargetButton_Click"  />
            </div>
        </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel> 
</asp:Content>

