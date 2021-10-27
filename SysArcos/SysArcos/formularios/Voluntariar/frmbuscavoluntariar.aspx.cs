﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            private void validapermisao(string pagina)
            {
                using (ARCOS_Entities conn new ARCOS_Entities())
                       
            {
                string login = (string)Session["usuariologado"];

                USUARIO u = entity.USUARIO.FirstOrDefault(linha => linha.LOGIN.Equals(login));
                if (!u.ADM)
                {
                    SISTEMA_ENTIDADE item = entity.SISTEMA_ENTIDADE.FirstOrDefault(x => .URL.Equals(pagina));)
                    if (item != null)
                    {
                        SISTEMA_ITEM_ENTIDADE perm = u.GRUPO.PERMISSÃO.SISTEMA_ITEM_ENTIDADE.FirstOrDefault(x => x.ID_SISTEMA_ENTIDADE.ToString().Equals(item.ID.ToString()));
                        if (perm == null)
                        {
                            Response.Redirect("/permissaonegada.aspx");
                        }
                    }

                }
            }
        }
    }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (ARCOS_Entities entities = new ARCOS_Entities())
            {
                List<VOLUNTARIAR> lista = null;
                if (txtBusca.Text == string.Empty)
                {
                    lista = entities.VOLUNTARIAR.ToList();
                }
                else if (rbVoluntariado.Checked)
                {
                    lista = entities.VOLUNTARIAR.Where(x => x.VOLUNTARIADO.DESCRICAO.StartsWith(txtBusca.Text)).ToList();
                }
                else if (rbVoluntario.Checked)
                {
                    lista = entities.VOLUNTARIAR.Where(x => x.VOLUNTARIO.NOME.StartsWith(txtBusca.Text)).ToList();
                }
                grid.DataSource = lista.OrderBy(x => x.DATA_INICIAL);
                grid.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmvoluntariar.aspx");
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            ARCOS_Entities conexao = new ARCOS_Entities();

            int IDSelecionado = Convert.ToInt32(grid.SelectedValue.ToString());

            VOLUNTARIAR aluno = conexao.VOLUNTARIAR.FirstOrDefault(
                    linha => linha.ID.ToString().Equals(IDSelecionado.ToString())
                    );

            conexao.VOLUNTARIAR.Remove(aluno);

            conexao.SaveChanges();

            atualizaGrid(conexao);
        }

        private void atualizaGrid(ARCOS_Entities conexao)
        {
            var lista = conexao.VOLUNTARIAR.ToList();
            grid.DataSource = lista;
            grid.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedValue != null)
                //Redireciona para a página de cadastro com o login como parâmtro
                Response.Redirect("frmvoluntariar.aspx?id=" + grid.SelectedValue.ToString());
        }
    }
}