﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GymManagementSystem
{
    public partial class Instructor : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pubz\Documents\GymDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        SqlDataAdapter DA;
        DataSet DS = null;
        BindingSource bindingSource2 = new BindingSource();

        public Instructor()
        {
            InitializeComponent();
            LoadAllCustomer();
        }
        private void LoadAllCustomer()
        {
            try
            {
                DS = new DataSet();
                bindingSource2.DataSource = null;

                con.Open();
                string qry = "Select * from Instructor ";

                DA = new SqlDataAdapter(qry, con);

                DA.Fill(DS, "InstructorDetails");
                bindingSource2.DataSource = DS.Tables["InstructorDetails"];
                InstructorDetailsGrid.DataSource = bindingSource2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured : " + ex);
            }
            finally
            {
                con.Close();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login two = new Login();
            this.Hide();
            two.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Home three = new Home();
            this.Hide();
            three.Show();
        }

        private void btnInstructor_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer four = new Customer();
            this.Hide();
            four.Show();
        }

        private void btnAccessories_Click(object sender, EventArgs e)
        {
            Accessories six = new Accessories();
            this.Hide();
            six.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            Attendance seven = new Attendance();
            this.Hide();
            seven.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Payment eight = new Payment();
            this.Hide();
            eight.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtInstructorAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtSearchInstructor.Text = "";
            LoadAllCustomer();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM Customer where CustomerID='" + txtInstructorID.Text + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);

            try
            {
                con.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataTable DS = new DataTable();
                DA.Fill(DS);


                if (DS.Rows.Count == 1)
                {
                    MessageBox.Show("This Instructor already exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generated : " + ex);
            }
            finally
            {
                con.Close();
            } 

            string gender = "Female";

            if (radioInstructorMale.Checked)
                gender = "Male";

            string query = "INSERT INTO Instructor VALUES ('" + txtInstructorID.Text + "','" + txtInstructorName.Text + "','" + txtInstructorAddress.Text + "','" + txtInstructorNIC.Text + "','" + txtInstructorEmail.Text+ "','" + txtInstructorTelNo.Text + "','" + gender + "')";
            SqlCommand comd = new SqlCommand(query, con);
            try
            {
                con.Open();
                comd.ExecuteNonQuery();
                MessageBox.Show("Instructor Registered Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured : " + ex);
            }
            finally
            {
                con.Close();
                InstructorDetailsGrid.DataSource = null;
                LoadAllCustomer();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string InsName = "";
            string address = "";
            string nic = "";
            string email = "";
            string no = "";

            string qry = "UPDATE Instructor SET InstructorName = @InsName, Address=@add, NIC=@nic, Email=@mail, Phone=@no  Where InstructorID = @insID";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();
                DataTable Dt = new DataTable();
                InstructorDetailsGrid.DataSource = bindingSource2;

                foreach (DataGridViewRow row in InstructorDetailsGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(txtInstructorID.Text))
                        {
                            DataRow dr = Dt.NewRow();

                            InsName = row.Cells[1].Value.ToString();
                            address = row.Cells[2].Value.ToString();
                            nic = row.Cells[3].Value.ToString();
                            email = row.Cells[4].Value.ToString();
                            no = row.Cells[5].Value.ToString();
                            break;
                        }
                    }
                }

                if (txtInstructorName.Text != null && txtInstructorName.Text != String.Empty)
                {
                    InsName = txtInstructorName.Text;
                }

                if (txtInstructorAddress.Text != null && txtInstructorAddress.Text != String.Empty)
                {
                    address = txtInstructorAddress.Text;
                }

                if (txtInstructorNIC.Text != null && txtInstructorNIC.Text != String.Empty)
                {
                    nic = txtInstructorNIC.Text;
                }

                if (txtInstructorEmail.Text != null && txtInstructorEmail.Text != String.Empty)
                {
                    email = txtInstructorEmail.Text;
                }

                if (txtInstructorTelNo.Text != null && txtInstructorTelNo.Text != String.Empty)
                {
                    no = txtInstructorTelNo.Text;
                }

                cmd.Parameters.AddWithValue("@InsName", InsName);
                cmd.Parameters.AddWithValue("@add", address);
                cmd.Parameters.AddWithValue("@nic", nic);
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@no", no);
                cmd.Parameters.AddWithValue("@insID", txtInstructorID.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generated " + ex);
            }
            finally
            {
                con.Close();
                InstructorDetailsGrid.DataSource = null;
                LoadAllCustomer();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchInstructor.Text;

            InstructorDetailsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                DataTable Dt = new DataTable();
                InstructorDetailsGrid.DataSource = bindingSource2;

                foreach (DataGridViewRow row in InstructorDetailsGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            // record exists   
                            Dt.Columns.Add("Instructor ID");
                            Dt.Columns.Add("Instructor Name");
                            Dt.Columns.Add("Address");
                            Dt.Columns.Add("NIC");
                            Dt.Columns.Add("Email");
                            Dt.Columns.Add("Phone");
                            Dt.Columns.Add("Gender");

                            DataRow dr = Dt.NewRow();
                            dr[0] = row.Cells[0].Value;
                            dr[1] = row.Cells[1].Value;
                            dr[2] = row.Cells[2].Value;
                            dr[3] = row.Cells[3].Value;
                            dr[4] = row.Cells[4].Value;
                            dr[5] = row.Cells[5].Value;
                            dr[6] = row.Cells[6].Value;

                            Dt.Rows.Add(dr);
                            break;
                        }
                    }
                }

                InstructorDetailsGrid.DataSource = Dt;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInstructorID.Text = "";
            txtInstructorName.Text = "";
            txtInstructorAddress.Text = "";
            txtInstructorNIC.Text = "";
            txtInstructorEmail.Text = "";
            txtInstructorTelNo.Text = "";
            radioInstructorFemale.Checked = false;
            radioInstructorMale.Checked = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string qry = "DELETE FROM Instructor WHERE InstructorID='" + txtInstructorID.Text + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generated " + ex);
            }
            finally
            {
                con.Close();
                InstructorDetailsGrid.DataSource = null;
                LoadAllCustomer();
            }
        }
    }
}
