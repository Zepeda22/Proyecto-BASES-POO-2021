﻿using MaterialSkin;
using MaterialSkin.Controls;
using Model.SQLSERVERContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class frmLogin : MaterialForm
    {
        
        public Controller.LoginController loginController = new Controller.LoginController();
        public LoginTime loginTimeRef = new LoginTime();
        public Administrator administratorRef = new Administrator();
        public Cabin cabinRef;
        public Employee employeeRef;
        public frmLogin()
        {
            InitializeComponent();
            
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            cmbAddressCabin.DataSource = loginController.GetAddressCabin();
            cmbAddressCabin.DisplayMember = "AddressCabin";
            cmbAddressCabin.ValueMember = "Id";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (loginController.GetAdministratorExists(txtUser.Text, txtPassword.Text))
            {
                /*MessageBox.Show("Bienvenido!", "Clinica UCA",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                //DateTime dateTime = new DateTime();
                administratorRef = loginController.GetAdministrator(txtUser.Text, txtPassword.Text);
                employeeRef = loginController.GetEmployee(administratorRef);

                cabinRef = loginController.GetCabin(administratorRef);
                //loginTimeRef.Id = 1;
                DateTime dateTime = DateTime.Now;
                loginTimeRef.DateTimeLogin = dateTime;
                loginTimeRef.IdAdministrator = administratorRef.Id;
                loginTimeRef.IdCabin = cabinRef.Id;
                loginController.SetLoginTime(loginTimeRef);
                
                var window = new frmPrincipal(administratorRef,cabinRef,loginTimeRef);
                window.ShowDialog();
                this.Hide();


            }
            else
                MessageBox.Show("Usuario no existe!", "Cabina UCA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 


        }

        private void cmbAddressCabin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
