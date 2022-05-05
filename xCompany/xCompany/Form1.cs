using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xCompany
{
    public partial class Form1 : Form
    {
        Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var wh = from w in Ent.Warehouses
                     select w.WH_Name;
            var prod = from p in Ent.Products
                     select p.P_code;
            var sup = from s in Ent.Suppliers
                           select s.S_id;
            var per = from permission in Ent.S_permission
                      select permission.Per_id_S;
            var c = from Client in Ent.Clients
                    select Client.C_id;

            foreach (var warehouse in wh)
            {
                comboBox1.Items.Add(warehouse);
                comboBox5.Items.Add(warehouse);
                comboBox6.Items.Add(warehouse);


            }
            foreach (var product in prod)
            {
                comboBox2.Items.Add(product);
            }
            foreach (var clt in c)
            {
                comboBox4.Items.Add(clt);
            }
            foreach (var supplier in sup)
            {
                comboBox3.Items.Add(supplier);
                comboBox7.Items.Add(supplier);

            }


            dataGridView1.DataSource = Ent.S_permission.ToList();
            dataGridView2.DataSource = Ent.C_permission.ToList();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
 
            textBox29.ReadOnly = true;
            textBox30.ReadOnly = true;
            textBox39.ReadOnly = true;
            textBox40.ReadOnly = true;
            //this.reportViewer1.RefreshReport();

            //this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) // warehouse combobox
        {
            string name = comboBox1.Text;
          ///  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var wh = Ent.Warehouses.Find(name);
            textBox1.Text = wh.WH_Name;
            textBox2.Text = wh.WH_Address;
            textBox3.Text = wh.WH_Manager;

        }

        private void Button1_Click(object sender, EventArgs e) //ADD Warehouse
        {
            Boolean notDuplicated = true;
            Warehouse wh = new Warehouse();
            wh.WH_Name = textBox1.Text;
            wh.WH_Address = textBox2.Text;
            wh.WH_Manager = textBox3.Text;
           // Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            foreach(var w in Ent.Warehouses)
            {
                if (w.WH_Name == wh.WH_Name)
                    notDuplicated = false;
            }
            if(notDuplicated == true)
            {
                Ent.Warehouses.Add(wh);
                Ent.SaveChanges();
                textBox1.Text = textBox2.Text = textBox3.Text = comboBox1.Text = string.Empty;
                comboBox1.Items.Add(wh.WH_Name);
                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("Duplicated Warehouse Name");
            }


        }

        private void Button2_Click(object sender, EventArgs e) //Update warehouse
        {
            
            string name = textBox1.Text;
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var wh = Ent.Warehouses.Find(name);
            if(wh!= null)
            {
                wh.WH_Name = textBox1.Text;
                wh.WH_Address = textBox2.Text;
                wh.WH_Manager = textBox3.Text;
                Ent.SaveChanges();
                textBox1.Text = textBox2.Text = textBox3.Text = string.Empty;
                MessageBox.Show("Updated Successfully");
            }
            else
            {
                MessageBox.Show("Warehouse not found");
            }

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e) //Product Combobox
        {
            listBox1.Items.Clear();
            int code = int.Parse(comboBox2.Text);
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var p = Ent.Products.Find(code);
            //var pm = Ent.Prod_Measure.Find(code);
            var pm = (from pr in Ent.Prod_Measure
                               where pr.P_code == code
                               select pr.Measure_Unit).First();
            var pm2 = from pr in Ent.Prod_Measure
                      where pr.P_code == code
                      select pr.Measure_Unit;
            textBox4.Text = p.P_code.ToString();
            textBox5.Text = p.P_name;
            string[] d3 = p.Prod_Date.ToString().Split(null);
            string s3 = string.Join(" ", d3[0]);
            textBox6.Text =s3;
            string[] d4 = p.Exp_Date.ToString().Split(null);
            string s4 = string.Join(" ", d4[0]);
            textBox7.Text = s4;
            textBox8.Text = pm;
            foreach (var prod_meas in pm2)
            {
                listBox1.Items.Add(prod_meas);
            }
        }

        private void Button4_Click(object sender, EventArgs e) //ADD PRODUCT
        {
            Boolean notDuplicated = true;
            Boolean notDuplicated2 = true;

            Product prod = new Product();
            Prod_Measure pm = new Prod_Measure();
            prod.P_code = int.Parse(textBox4.Text);
            prod.P_name = textBox5.Text;
            prod.Prod_Date = DateTime.Parse(textBox6.Text);
            prod.Exp_Date = DateTime.Parse(textBox7.Text);

            pm.P_code = int.Parse(textBox4.Text);
            pm.Measure_Unit = textBox8.Text;

            //Prod_WareHouse pw = new Prod_WareHouse();
            //pw.P_Code = int.Parse(textBox4.Text);
            //pw.WH_Name = comboBox1.Text;
            //Ent.Prod_WareHouse.Add();
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            foreach (var P in Ent.Products)
            {
                if (P.P_code == prod.P_code)
                    notDuplicated = false;
            }
            var ms = from n in Ent.Prod_Measure
                     where n.P_code == prod.P_code
                     select n.Measure_Unit;
                     
            foreach (var m in ms)
            {
                if (m == pm.Measure_Unit)
                    notDuplicated2 = false;
            }
            if (notDuplicated == true && notDuplicated2 == true)
            {
                Ent.Products.Add(prod);
                Ent.Prod_Measure.Add(pm);
                Ent.SaveChanges();
                textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                listBox1.Items.Add(pm.Measure_Unit);
                comboBox2.Items.Add(prod.P_code);
                MessageBox.Show("Added Successfully");
            }
            else if (notDuplicated == false && notDuplicated2 == true)
            {
                Ent.Prod_Measure.Add(pm);
                listBox1.Items.Add(pm.Measure_Unit);
                Ent.SaveChanges();
                textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                listBox1.Items.Clear();

                MessageBox.Show("Measurement Added Successfully");
                
            }
            else
            {
                MessageBox.Show("Duplicated Data");
            }

        }

        private void Button3_Click(object sender, EventArgs e) //update product
        {
            int code = int.Parse(textBox4.Text);
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var pr = Ent.Products.Find(code);
            var pm = (from prod in Ent.Prod_Measure
                          where prod.P_code == code
                          select prod.Measure_Unit).First();
            var prod1 = Ent.Prod_Measure.Find(pm, code);
            

            if (pr != null)
            {
                Ent.Prod_Measure.Remove(prod1);
                listBox1.Items.Remove(prod1.Measure_Unit);
                Prod_Measure product = new Prod_Measure();
                product.P_code = int.Parse(textBox4.Text);
                product.Measure_Unit = textBox8.Text;
                Ent.Prod_Measure.Add(product);
                listBox1.Items.Add(product.Measure_Unit);
                pr.P_code = int.Parse(textBox4.Text);
                pr.P_name = textBox5.Text;
                pr.Prod_Date = DateTime.Parse(textBox6.Text);
                pr.Exp_Date = DateTime.Parse(textBox7.Text);
             
               // prod1.Measure_Unit = textBox8.Text;
                Ent.SaveChanges();
                textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = comboBox2.Text =string.Empty;
                listBox1.Items.Clear();
                MessageBox.Show("Updated Successfully");
            }
            else
            {
                MessageBox.Show("Product not found");
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e) //supplier combobox
        {
            int s_id = int.Parse(comboBox3.Text);     
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var sup = Ent.Suppliers.Find(s_id);
            textBox9.Text = sup.S_id.ToString();
            textBox10.Text = sup.S_email;
            textBox11.Text = sup.S_name;
            textBox12.Text = sup.S_fax.ToString();
            textBox13.Text = sup.S_mob_num.ToString();
            textBox14.Text = sup.S_tel_num.ToString();
            textBox15.Text = sup.S_website;

        }

        private void Button5_Click(object sender, EventArgs e) //Add Supplier
        {
            Boolean notDuplicated = true;
            Supplier sup = new Supplier();
            sup.S_id = int.Parse(textBox9.Text);
            sup.S_email = textBox10.Text;
            sup.S_name = textBox11.Text;
            sup.S_fax = int.Parse(textBox12.Text);
            sup.S_mob_num = int.Parse(textBox13.Text);
            sup.S_tel_num = int.Parse(textBox14.Text);
            sup.S_website = textBox15.Text;

         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            foreach (var s in Ent.Suppliers)
            {
                if (s.S_id == sup.S_id)
                    notDuplicated = false;
            }
            if (notDuplicated == true)
            {
                Ent.Suppliers.Add(sup);
                Ent.SaveChanges();
                textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text = string.Empty;
                comboBox3.Items.Add(sup.S_id);
                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("Duplicated Supplier");
            }
        }

        private void Button6_Click(object sender, EventArgs e) //Supplier Update
        {
            int s_id = int.Parse(textBox9.Text);
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();

            var sup = Ent.Suppliers.Find(s_id);
            if(sup != null)
            {
                sup.S_id = int.Parse(textBox9.Text);
                sup.S_email = textBox10.Text;
                sup.S_name = textBox11.Text;
                sup.S_fax = int.Parse(textBox12.Text);
                sup.S_mob_num = int.Parse(textBox13.Text);
                sup.S_tel_num = int.Parse(textBox14.Text);
                sup.S_website = textBox15.Text;
                Ent.SaveChanges();
                textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text = string.Empty;
                MessageBox.Show("Updated Successfully");
            }
            else
            {
                MessageBox.Show("Supplier not found");
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e) //client combobox
        {
            int c_id = int.Parse(comboBox4.Text);
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var client = Ent.Clients.Find(c_id);
            textBox16.Text = client.C_id.ToString();
            textBox17.Text = client.C_email;
            textBox18.Text = client.C_name;
            textBox19.Text = client.C_fax.ToString();
            textBox20.Text = client.C_mob_num.ToString();
            textBox21.Text = client.C_tel_num.ToString();
            textBox22.Text = client.C_website;
        }

        private void Button8_Click(object sender, EventArgs e) //add client
        {

            Boolean notDuplicated = true;
            Client client = new Client();
            client.C_id = int.Parse(textBox16.Text);
            client.C_email = textBox17.Text;
            client.C_name = textBox18.Text;
            client.C_fax = int.Parse(textBox19.Text);
            client.C_mob_num = int.Parse(textBox20.Text);
            client.C_tel_num = int.Parse(textBox21.Text);
            client.C_website = textBox22.Text;
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            foreach (var c in Ent.Clients)
            {
                if (c.C_id == client.C_id)
                    notDuplicated = false;
            }
            if (notDuplicated == true)
            {
                Ent.Clients.Add(client);
                Ent.SaveChanges();
                textBox16.Text = textBox17.Text = textBox18.Text = textBox19.Text = textBox20.Text = textBox21.Text = textBox22.Text = string.Empty;
                comboBox4.Items.Add(client.C_id);
                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("Duplicated Client");
            }
        }

        private void Button7_Click(object sender, EventArgs e) //Client Update
        {
            int c_id = int.Parse(textBox16.Text);
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();

            var client = Ent.Clients.Find(c_id);
            if (client != null)
            {
                client.C_id = int.Parse(textBox16.Text);
                client.C_email = textBox17.Text;
                client.C_name = textBox18.Text;
                client.C_fax = int.Parse(textBox19.Text);
                client.C_mob_num = int.Parse(textBox20.Text);
                client.C_tel_num = int.Parse(textBox21.Text);
                client.C_website = textBox22.Text;
                Ent.SaveChanges();
                textBox16.Text = textBox17.Text = textBox18.Text = textBox19.Text = textBox20.Text = textBox21.Text = textBox22.Text = string.Empty;
                MessageBox.Show("Updated Successfully");
            }
            else
            {
                MessageBox.Show("Client not found");
            }
        }




        //private void DataGridView1_SelectionChanged(object sender, EventArgs e) //filling the import permission form
        //{
        //    Linq_ProjectEntities1 Ent = new Linq_ProjectEntities1();
            

        //    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        //    {
        //        var p = Ent.Products.Find(row.Cells[2].Value); //product
        //        var pd = Ent.S_permission.Find(row.Cells[0].Value,p.P_code); //permission

 
        //        var q = (from quant in Ent.Sup_Quantity
        //                  where (quant.P_code == pd.P_code) && (quant.Per_id_S == pd.Per_id_S)
        //                  select quant.quantity).First();

        //        var supName = (from sup in Ent.Suppliers
        //                       where sup.S_id == pd.S_id
        //                       select sup.S_name).First();
        //        textBox23.Text = row.Cells[1].Value.ToString();
        //        textBox24.Text = row.Cells[0].Value.ToString();
        //        textBox26.Text = row.Cells[2].Value.ToString();
        //        textBox25.Text = row.Cells[3].Value.ToString();
        //        textBox28.Text = supName;
        //        textBox27.Text = q.ToString();
        //        textBox29.Text = p.Prod_Date.ToString();
        //        textBox30.Text = (p.Exp_Date - p.Prod_Date).ToString();


        //    }
        //}


        private void Button9_Click(object sender, EventArgs e) //Add for import permission
        {
        //    Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            Boolean notDuplicated = true;
            Boolean validData = false;
            S_permission sp = new S_permission();
            Sup_Quantity sq = new Sup_Quantity();
            sp.WH_Name = textBox23.Text;
            sp.Per_id_S = int.Parse(textBox24.Text);
            sp.S_Per_date = DateTime.Parse(textBox25.Text);
            sp.S_id = (from n in Ent.Suppliers
                       where n.S_name == textBox28.Text
                       select n.S_id).First();
            sp.P_code = int.Parse(textBox26.Text);
            sq.quantity = int.Parse(textBox27.Text);
            sq.WH_Name = textBox23.Text;
            sq.P_code = int.Parse(textBox26.Text);
            sq.S_id = (from n in Ent.Suppliers
                       where n.S_name == textBox28.Text
                       select n.S_id).First();
            sq.Per_id_S = int.Parse(textBox24.Text);

            foreach (var s in Ent.S_permission)
            {
                if ((s.Per_id_S == sp.Per_id_S) && (s.P_code == sp.P_code))
                {
                    notDuplicated = false;
                }

            }
            foreach(var w in Ent.Warehouses)
            {
                   if (sp.WH_Name == w.WH_Name)
                {
                    validData = true;
                }
            }
            foreach (var s in Ent.Suppliers)
            {
                if ((sp.S_id == s.S_id))
                {
                    validData = true;
                }
            }
            foreach (var p in Ent.Products)
            {
                if ((sp.P_code == p.P_code))
                {
                    validData = true;
                }
            }
            if (notDuplicated == true && validData == true)
            {
                Ent.S_permission.Add(sp);
                Ent.Sup_Quantity.Add(sq);
                Ent.SaveChanges();
                textBox23.Text = textBox24.Text = textBox25.Text = textBox26.Text = textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text = string.Empty;

                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("Duplicated permission id & Product Code or not valid data");
            }
            dataGridView1.DataSource = Ent.S_permission.ToList();
        }

        private void Button10_Click(object sender, EventArgs e) ////Edit for import permission
        {
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var p = Ent.Products.Find(int.Parse(textBox26.Text)); //permission id
            var sp = Ent.S_permission.Find(int.Parse(textBox24.Text), p.P_code); //prod id

            
            var q = (from quant in Ent.Sup_Quantity
                     where (quant.P_code == sp.P_code) && (quant.Per_id_S == sp.Per_id_S)
                     select quant.quantity).First();
            var sq = Ent.Sup_Quantity.Find(sp.P_code, q, sp.Per_id_S);

            sp.WH_Name = textBox23.Text;
            sp.Per_id_S = int.Parse(textBox24.Text);
            sp.S_Per_date = DateTime.Parse(textBox25.Text);
            sp.S_id = (from n in Ent.Suppliers
                       where n.S_name == textBox28.Text
                       select n.S_id).First();
            sp.P_code = int.Parse(textBox26.Text);
       
            sq.quantity = int.Parse(textBox27.Text);
            sq.WH_Name = textBox23.Text;
            sq.P_code = int.Parse(textBox26.Text);
       
            sq.S_id = (from n in Ent.Suppliers
                       where n.S_name == textBox28.Text
                       select n.S_id).First();
            sq.Per_id_S = int.Parse(textBox24.Text);
            Ent.SaveChanges();
            textBox23.Text = textBox24.Text = textBox25.Text = textBox26.Text = textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text = string.Empty;
            MessageBox.Show("Updated Successfully");
            dataGridView1.DataSource = Ent.S_permission.ToList();
        }



        private void Button12_Click(object sender, EventArgs e) //Add to export permission
        {
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            Boolean notDuplicated = true;
            Boolean validData = false;
            C_permission cp = new C_permission();
            Client_Quantity cq = new Client_Quantity();
            cp.WH_name = textBox38.Text;
            cp.Per_id_C = int.Parse(textBox37.Text);
            cp.C_per_date = DateTime.Parse(textBox36.Text);
            cp.C_id = (from n in Ent.Clients
                       where n.C_name == textBox35.Text
                       select n.C_id).First();
            cp.P_code = int.Parse(textBox33.Text);
            cq.quantity = int.Parse(textBox34.Text);
            cq.WH_Name = textBox38.Text;
            cq.P_code = int.Parse(textBox33.Text);
            cq.C_id = (from n in Ent.Clients
                       where n.C_name == textBox35.Text
                       select n.C_id).First();
            cq.Per_id_C = int.Parse(textBox37.Text);

            foreach (var c in Ent.C_permission)
            {
                if ((c.Per_id_C == cp.Per_id_C) && (c.P_code == cp.P_code))
                {
                    notDuplicated = false;
                }

            }
            foreach (var w in Ent.Warehouses)
            {
                if (cp.WH_name == w.WH_Name)
                {
                    validData = true;
                }
            }
            foreach (var c in Ent.Clients)
            {
                if (cp.C_id == c.C_id)
                {
                    validData = true;
                }
            }
            foreach (var p in Ent.Products)
            {
                if (cp.P_code == p.P_code)
                {
                    validData = true;
                }
            }
            if (notDuplicated == true && validData == true)
            {
                Ent.C_permission.Add(cp);
                Ent.Client_Quantity.Add(cq);
                Ent.SaveChanges();
                textBox33.Text = textBox34.Text = textBox35.Text = textBox36.Text = textBox37.Text = textBox38.Text = string.Empty;

                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("Duplicated permission id & Product Code or not valid data");
            }
            dataGridView2.DataSource = Ent.C_permission.ToList();

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();


            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                var p = Ent.Products.Find(row.Cells[2].Value); //product
                var pd = Ent.S_permission.Find(row.Cells[0].Value, p.P_code); //permission


                var q = (from quant in Ent.Sup_Quantity
                         where (quant.P_code == pd.P_code) && (quant.Per_id_S == pd.Per_id_S)
                         select quant.quantity).First();

                var supName = (from sup in Ent.Suppliers
                               where sup.S_id == pd.S_id
                               select sup.S_name).First();
                textBox23.Text = row.Cells[1].Value.ToString();
                textBox24.Text = row.Cells[0].Value.ToString();
                textBox26.Text = row.Cells[2].Value.ToString();
                string[] d1 = row.Cells[3].Value.ToString().Split(null);
                string s1 = string.Join(" ", d1[0]);
                textBox25.Text = s1;
                textBox28.Text = supName;
                textBox27.Text = q.ToString();
                string[] d2 = p.Prod_Date.ToString().Split(null);
                string s2 = string.Join(" ", d2[0]);
                textBox29.Text = s2;
                string[] d3 = (p.Exp_Date - p.Prod_Date).ToString().Split('.');
                string s3 = string.Join(" ", d3[0]);
                textBox30.Text = s3;


            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) //filling the export permission form
        {
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                var p = Ent.Products.Find(row.Cells[2].Value); //product
                var pd = Ent.C_permission.Find(row.Cells[0].Value, p.P_code); //permission 


                var q = (from qu in Ent.Client_Quantity
                         where (qu.P_code == pd.P_code) && (qu.Per_id_C == pd.Per_id_C)
                         select qu.quantity).First();


                var ClientName = (from cl in Ent.Clients
                                  where (cl.C_id == pd.C_id)
                                  select cl.C_name).First();
                textBox38.Text = row.Cells[1].Value.ToString();
                textBox37.Text = row.Cells[0].Value.ToString();
                textBox33.Text = row.Cells[2].Value.ToString();
                string[] d3 = row.Cells[3].Value.ToString().Split(null);
                string s3 = string.Join(" ", d3[0]);
                textBox36.Text = s3;
                textBox35.Text = ClientName;
                textBox34.Text = q.ToString();

            }
        }

        private void Button11_Click(object sender, EventArgs e) //updating the export permission
        {
         //   Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            var p = Ent.Products.Find(int.Parse(textBox33.Text)); //product
            var cp = Ent.C_permission.Find(int.Parse(textBox37.Text), p.P_code); //prmission 


            var q = (from quant in Ent.Client_Quantity
                     where (quant.P_code == cp.P_code) && (quant.Per_id_C == cp.Per_id_C)
                     select quant.quantity).First();
            var cq = Ent.Client_Quantity.Find(cp.P_code, q, cp.Per_id_C);

            cp.WH_name = textBox38.Text;
            cp.Per_id_C = int.Parse(textBox37.Text);
            cp.C_per_date = DateTime.Parse(textBox36.Text);
            cp.C_id = (from n in Ent.Clients
                       where n.C_name == textBox35.Text
                       select n.C_id).First();
            cp.P_code = int.Parse(textBox33.Text);

            cq.quantity = int.Parse(textBox34.Text);
            cq.WH_Name = textBox38.Text;
            cq.P_code = int.Parse(textBox33.Text);

            cq.C_id = (from n in Ent.Clients
                       where n.C_name == textBox35.Text
                       select n.C_id).First();
            cq.Per_id_C = int.Parse(textBox37.Text);
            Ent.SaveChanges();
            textBox33.Text = textBox34.Text = textBox35.Text = textBox36.Text = textBox37.Text = textBox38.Text = string.Empty;
            MessageBox.Show("Updated Successfully");
            dataGridView2.DataSource = Ent.C_permission.ToList();
        }

        private void Button13_Click(object sender, EventArgs e) //transfer button
        {

            if (comboBox5.SelectedItem == comboBox6.SelectedItem)
            {
                MessageBox.Show("please enter a valid warehouse");
            }
            else
            {
                prod_Transfer pt = new prod_Transfer();
                pt.from_wh = comboBox5.Text;
                pt.to_wh = comboBox6.Text;
                pt.p_code = int.Parse(textBox31.Text);
                pt.quantity= int.Parse(textBox32.Text);
                pt.supplierID = int.Parse(comboBox7.Text);
                pt.transferDate = DateTime.Parse(textBox41.Text);
                Ent.prod_Transfer.Add(pt);
                Ent.SaveChanges();
                comboBox5.Text = comboBox6.Text = comboBox7.Text = textBox31.Text = textBox32.Text = textBox39.Text = textBox40.Text = textBox41.Text = string.Empty;

                MessageBox.Show("Transfered Successfully");


            }
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
          //  Linq_ProjectEntities3 Ent = new Linq_ProjectEntities3();
            
            if (String.IsNullOrEmpty(textBox31.Text)== false )
            {

                var prod = Ent.Products.Find(int.Parse(textBox31.Text));
                if(prod != null)
                {
                    string[] d1 = prod.Prod_Date.ToString().Split(null);
                    string s1 = string.Join(" ", d1[0]);
                    textBox39.Text =s1;

                    string[] d2 = (prod.Exp_Date - prod.Prod_Date).ToString().Split('.');
                    string s2 = string.Join(" ", d2[0]);

                    textBox40.Text = s2;

                }

            }

        }



        private void Button14_Click(object sender, EventArgs e) //warehouses
        {
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport1&rs:Command=Render");
        }

        private void Button16_Click(object sender, EventArgs e) //products
        {
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport2&rs:Command=Render");
        }

        private void Button17_Click(object sender, EventArgs e)//Warehouses count for each product
        {
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport3&rs:Command=Render");

        }

        private void Button15_Click(object sender, EventArgs e)//product transfer
        {
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport4&rs:Command=Render");
        }

        private void Button18_Click(object sender, EventArgs e)//days passed since the product in store
        {
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport5&rs:Command=Render");

        }

        private void Button19_Click(object sender, EventArgs e)//products close to expiration
        {
        
            System.Diagnostics.Process.Start("http://localhost/ReportServer/Pages/ReportViewer.aspx?%2fReportxCompany%2fReport6&rs:Command=Render");

        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox26.Text) == false)
            {

                var prod = Ent.Products.Find(int.Parse(textBox26.Text));
                if (prod != null)
                {
                    string[] d1 = prod.Prod_Date.ToString().Split(null);
                    string s1 = string.Join(" ", d1[0]);
                    textBox29.Text = s1;

                    string[] d2 = (prod.Exp_Date - prod.Prod_Date).ToString().Split('.');
                    string s2 = string.Join(" ", d2[0]);

                    textBox30.Text = s2;

                }

            }
        }
    }
}
