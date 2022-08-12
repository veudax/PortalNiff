<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Site.Cadastros.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="">
                <asp:Label runat="server" text="Úsuário"> </asp:Label>
            </div>
            <div>
                <asp:TextBox runat="server" text=""></asp:TextBox>
            </div>
            <div>
                <asp:Label runat="server" text="Nome"> </asp:Label>
            </div>
            <div>
                <asp:TextBox runat="server" text=""></asp:TextBox>
            </div>
            <div class="opcoes">
                <div>
                    <asp:Label runat="server" Text="Tipo"></asp:Label>
                </div>
                <asp:DropDownList runat="server">
                    <asp:ListItem>Solicitante</asp:ListItem>
                    <asp:ListItem>Atendente</asp:ListItem>
                    <asp:ListItem>Ambos</asp:ListItem>
                </asp:DropDownList>

                <div class="checkbox">
                    <p>Opções</p>
                    <asp:CheckBox runat="server" Text="Ativo" />
                    <asp:CheckBox runat="server" Text="Administrador" />
                    <asp:CheckBox runat="server" Text="Permite acessar a agenda" />
                    <asp:CheckBox runat="server" Text="Permite acessar o chat" />
                    <asp:CheckBox runat="server" Text="Permite excluir histórico de chat" />
                </div>
            </div>

                <asp:Label runat="server" text="IP da máquina"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>
                <asp:Label runat="server" text="Nome da máquina"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>

                <asp:Label runat="server" text="Telefone"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>
                <asp:Label runat="server" text="Ramal"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>

                <asp:Label runat="server" text="E-mail"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>
                <asp:Label runat="server" text="Cargo"> </asp:Label>
                <asp:TextBox runat="server" text=""></asp:TextBox>


            </div>
            <div class="botoes">

            </div>
        </div>
    </form>
</body>
</html>
