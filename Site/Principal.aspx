﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Site.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <%--<div class="barraTitulo">
            <p> Home > Lista de Chamados 
            </p>
        </div>
    --%>

    <%--<div class="containerPrincipal">
         
        <asp:GridView ID="chamadosGridView" runat="server" AutoGenerateDeleteButton="false" AutoGenerateEditButton="false"
            DataKeyNames="Numero" Visible="true" AllowPaging="True">
            <Columns>
                <asp:BoundField HeaderText="Nº Chamado" DataField="Numero" />
                <asp:BoundField HeaderText="Data" DataField="Data" />
                <asp:BoundField HeaderText="Categoria" DataField="IdCategoria"/>
                <asp:BoundField HeaderText="Modulo" />
                <asp:BoundField HeaderText="Tela" DataField="IdTela"/>
                <asp:BoundField HeaderText="Assunto" DataField="Assunto"/>
                <asp:BoundField HeaderText="Solicitante" DataField="NomeSolicitante"/>
                <asp:BoundField HeaderText="Status" DataField="Status"/>
                <asp:BoundField HeaderText="Responsável" />
                <asp:BoundField HeaderText="Origem" />
                <asp:BoundField HeaderText="Empresa" />
                <asp:BoundField HeaderText="Nº Adequação" />
                <asp:BoundField HeaderText="Prazo Adequação" />
                <asp:BoundField HeaderText="Prioridade" />
            </Columns>

        </asp:GridView>
    </div>--%>

</asp:Content>