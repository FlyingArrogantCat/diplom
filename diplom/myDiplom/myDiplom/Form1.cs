﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Data.SqlClient;

namespace myDiplom
{
    //public static List<string> mass_of_country; /*= new List<string>() { "Russia", "Finland" }*/
    public partial class Form1 : Form
    {
        //public static int Amount_of_country = 3;
        public static int max_life_age = 10;
        public static int amt_educ = 3;
        public matrix tens = new matrix(amt_educ, max_life_age);
        public country temp_country = new country();
        public List<country> Gomer = new List<country>();
        public matrix graph = new matrix(3, 3);
        public matrix input = new matrix(10, 3);
        public List<dynamic> dym=new List<dynamic>();
        //public matrix output = new matrix(10, 3);
        public void computing_matrix_change(List<country> Gomer)
        {
            for(int i=0;i<Gomer.Count;i++)
            {
                //Gomer[i].ch_age.self ={ };
            }
        }
    
        public int indicator_func(double a,double b)
        {
            int k = 0; 
            if (a>b)
            {
                k = 1;
                return k;
            }
            else
            {
                k = 0;
                return k;
            }
        }
        public double coeff(int a,int b)
        {
            return 1.0f*((-1)*(a*a-10*a+16)*indicator_func(a,2)*indicator_func(8,a)/(5*1.8f))*(b*1.0f/3);
        }
        public double normal(double[] x,double[] y)
        {
            double temp = 0.0f;
            for(int i=0;i<x.Length;i++)
            {
                temp += (x[i] - y[i]) * (x[i] - y[i]);
            }
            temp += Math.Sqrt(temp);
            return temp;
        }
        public void Arrival_immigrant(country count,matrix temp, int number)
        {
            matrix temptemp = new matrix(count.population.size_first, count.population.size_second);
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k <10; k++)
                    {
                        temptemp.self[i, j] +=count.ch_age.self[i,k]*count.population.self[k,j];
                    }
                }
                if (i==0)
                {
                    temptemp.self[i, 0] = temptemp.self[i, 0] + temptemp.self[i, 1] + temptemp.self[i, 2];
                    temptemp.self[i, 1] = 0.0f;
                    temptemp.self[i, 2] = 0.0f;
                }
            }
            double for_cult;
            for (int i = 0; i < 10; i++)
            {             
                for_cult = 0.0f;
                for (int j = 0; j < 3; j++)
                {
                    count.population.self[i, j] =  temptemp.self[i, j];
                    for_cult += temptemp.self[i, j];
                }
                count.culture.distrib[count.number, i] = for_cult;
            }
            /*
            count.population.self[0, 0] = count.population.self[0, 0] + count.population.self[0, 1] + count.population.self[0, 2];
            count.population.self[0, 1] = 0.0f;
            count.population.self[0, 2] = 0.0f;
            */
            for (int i=0;i<count.population.size_first;i++)
                for (int j = 0; j < count.population.size_second; j++)
                {
                    count.population.self[i,j] += Math.Round(temp.self[i,j]);
                }
            for_cult=0;
            for (int i = 0; i < count.population.size_first; i++)
            {
                for_cult = 0;
                for (int j = 0; j < count.population.size_second; j++)
                {
                    for_cult += Math.Round(temp.self[i, j]);
                }
                count.culture.distrib[number, i]+= for_cult;
            }
                  
        }
        public void Change_education(country temp)
        {
            matrix temptemp = new matrix(temp.population.size_first, temp.population.size_second);
            MessageBox.Show(temp.population.size_first.ToString()+'\t'+ temp.population.size_second.ToString(), "", MessageBoxButtons.OK);
            for (int i = 0; i < temp.population.size_first ; i++)//10
            {
                for (int j = 0; j < temp.population.size_second; j++)//3
                {
                    //temptemp.self[i, j] = 0.0f;
                    for (int k = 0; k < temp.population.size_second; k++)//3
                    {
                        //temp.population.self[i, j] = temp.ch_educ.self[i, k]*temp.population.self[k,j];
                        temptemp.self[i, j] += temp.ch_educ.self[k, j] * temp.population.self[i, k];
                    }
                }
            }

            temptemp.self[0, 0] = temp.ch_educ.self[0, 0];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //MessageBox.Show(temp.population.self[i, j].ToString()+'\n'+ temptemp.self[i, j].ToString(), "change Education ", MessageBoxButtons.OK);
                    temp.population.self[i,j] = temptemp.self[i,j];
                }
            }
        }
        public void Culture_assimilation(List<country> Gomer)
        {
            double for_cult = 0.0f;
            double[] moment = {0,0,0};
            for (int i=0;i<Gomer.Count;i++)
            {
                for (int k = 0; k < Gomer[i].culture.size_culture; k++)
                {
                    for_cult = 0.0f;
                    for (int j = 0; j < Gomer[i].culture.size_age; j++)
                    {
                        for_cult += Gomer[i].culture.distrib[k, j];
                    }
                    moment[k] = for_cult;
                    // MessageBox.Show(moment[k].ToString(), Gomer[i].name_country, MessageBoxButtons.OK);
                }

                for(int j=0;j<moment.Length;j++)
                {
                    moment[j] = moment[j] /** Gomer[i].culture.education[j]*//Gomer[i].culture.amt();
                    //MessageBox.Show(moment[j].ToString(), Gomer[i].name_country, MessageBoxButtons.OK);

                }

                double[,] temptemp = new double[Gomer[i].culture.size_culture,Gomer[i].culture.size_age];
                for (int l = 0; l < Gomer[i].culture.size_culture; l++)
                {
                    for (int j = 0; j < Gomer[i].culture.size_age; j++)
                    {
                        temptemp[l,j] = 0.0f;
                        for (int k = 0; k < Gomer[i].culture.size_culture; k++)
                        {
                            temptemp[l,j] += (indicator_func(30, j * 10) * moment[k] + indicator_func(j * 10, 30)) *Gomer[i].culture.education[l]* Gomer[i].culture.distrib[k, j];
                        }
                    }
                }
                for (int l = 0; l < Gomer[i].culture.size_culture; l++)
                {
                    for (int j = 0; j < Gomer[i].culture.size_age; j++)
                    {
                        Gomer[i].culture.distrib[l, j] = temptemp[l, j];
                        //MessageBox.Show(temptemp[l, j].ToString(), Gomer[i].name_country, MessageBoxButtons.OK);
                    }
                }
            }

        }


        public void Graph_calc(List<country> Gomer,matrix graph)
        {
            graph = new matrix(Gomer.Count, Gomer.Count);
            for(int i=0;i<Gomer.Count;i++)
            {
                for(int j=0;j<Gomer.Count;j++)
                {
                    double[] veci = {Gomer[i].power,Gomer[i].technology,Gomer[i].enviroment,Gomer[i].educ_tech,Gomer[i].educ_cult};
                    double[] vecj = { Gomer[j].power, Gomer[j].technology, Gomer[j].enviroment, Gomer[j].educ_tech, Gomer[j].educ_cult };
                    graph.self[i, j] = (indicator_func(Gomer[i].educ_cult,Gomer[j].educ_cult)+ indicator_func(Gomer[i].educ_tech, Gomer[j].educ_tech)+ indicator_func(Gomer[i].enviroment, Gomer[j].enviroment)+ indicator_func(Gomer[i].power, Gomer[j].power)+ indicator_func(Gomer[i].technology, Gomer[j].technology))*normal(veci,vecj)*0.2f/ (indicator_func(Gomer[i].educ_cult, Gomer[j].educ_cult) + indicator_func(Gomer[i].educ_tech, Gomer[j].educ_tech) + indicator_func(Gomer[i].enviroment, Gomer[j].enviroment) + indicator_func(Gomer[i].power, Gomer[j].power) + indicator_func(Gomer[i].technology, Gomer[j].technology+1.0f));
                    if (graph.self[i, j] > 0)
                    {
                        //MessageBox.Show(graph.self[i, j].ToString(), "Graph calc", MessageBoxButtons.OK);
                    }
                    else graph.self[i, j] = 0.0f;
                }
            }
        }
        public void defeintion_coeff_matrix(List<country> Gomer)
        {
            for(int i=0;i<Gomer.Count;i++)
            {
                for(int j=0;j<Gomer[i].ch_age.size_first;j++)
                {
                    for(int k = 0; k < Gomer[i].ch_age.size_second; k++)
                    {
                        if (j-1 == k)
                            Gomer[i].ch_age.self[j, k] = 0.95f;
                        if (j == 0)
                            Gomer[i].ch_age.self[j, k] = 0.4f*(-k*k+9*k-8)*indicator_func(k*1.0f,1.0f)*indicator_func(8.0f,k*1.0f)/12;
                        
                    }
                }
            }
            for (int i = 0; i < Gomer.Count; i++)
            {
                for (int j = 0; j < Gomer[i].ch_educ.size_first; j++)
                {
                    for (int k = 0; k < Gomer[i].ch_educ.size_second; k++)
                    {
                        Gomer[i].ch_educ.self[j, k] = 0.0f;
                        if (j == k)
                            Gomer[i].ch_educ.self[j, k] = 1 - Gomer[i].educ_tech;
                        if (j + 1 == k)
                            Gomer[i].ch_educ.self[j, k] = Gomer[i].educ_tech;
                    }
                }
                //MessageBox.Show(Gomer[i].ch_educ.self[0, 0].ToString()+'\t'+Gomer[i].ch_educ.self[0, 1].ToString() + '\t'+ Gomer[i].ch_educ.self[0, 2].ToString() + '\n'+ Gomer[i].ch_educ.self[1, 0].ToString() + '\t' + Gomer[i].ch_educ.self[1, 1].ToString() + '\t' + Gomer[i].ch_educ.self[1, 2].ToString()+'\n'+Gomer[i].ch_educ.self[2, 0].ToString() + '\t' + Gomer[i].ch_educ.self[2, 1].ToString() + '\t' + Gomer[i].ch_educ.self[2, 2].ToString(), "matrix",MessageBoxButtons.OK);
                Gomer[i].ch_educ.self[Gomer[i].ch_educ.size_first - 1, Gomer[i].ch_educ.size_second - 1] = 1.0f;

            }
            //MessageBox.Show(Gomer[0].ch_educ.self[0,0]+"\t"+ Gomer[0].ch_educ.self[0, 1] + "\t"+ Gomer[0].ch_educ.self[0, 2] + "\n"+ Gomer[0].ch_educ.self[1, 0] + "\t" + Gomer[0].ch_educ.self[1, 1] + "\t" + Gomer[0].ch_educ.self[1, 2]+ Gomer[0].ch_educ.self[2, 0] + "\t" + Gomer[0].ch_educ.self[2, 1] + "\t" + Gomer[0].ch_educ.self[2, 2], "Educ coeff", MessageBoxButtons.OK);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void createNewDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Form2 newForm = new Form2();
            newForm.Show();*/
        }

        private void addNewCountryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            add_new_country f = new add_new_country();
            f.Show();
            */
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*show_data f = new show_data();
            f.Show();*/
            /*add_country f = new add_country();
            f.Owner = this;
            f.ShowDialog();*/
            Filling_data f = new Filling_data();
            f.Owner = this;
            f.ShowDialog();
        }

        private void aboutAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Loginov Fyodor 471b group MIPT", "Info",MessageBoxButtons.OK);
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Diplom project","Info",MessageBoxButtons.OK);
        }

        private void mainIdeaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Here I must create reading data from database to GOmer*/
        }

        private void lastCommentFromMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "We can add cryptographic randomizer for our program using from 'System.Security.Cryptography' class 'RNGCryptoServiceProvider' ", "Comment", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic tt = new dynamic();
            for (int i=0;i<Gomer.Count;i++)
            {
                dym.Add(tt);
            }
            for (int rr = 0; rr < 1; rr++)
            {
                Graph_calc(Gomer, graph);
                defeintion_coeff_matrix(Gomer);
                
                for (int l = 0; l < Gomer.Count; l++)
                {
                    Change_education(Gomer[l]);
                }
                
                List<matrix> temp = new List<matrix>();
                for (int l = 0; l < Gomer.Count; l++)
                {
                    input = new matrix(10, 3);
                    for (int k = 0; k < Gomer.Count; k++)
                    {
                        if (k == l) continue;
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                input.self[i, j] = Math.Round(graph.self[l, k] * Gomer[l].population.self[i, j] * coeff(i, j));
                                if (input.self[i, j] > Gomer[l].population.self[i, j])
                                {
                                    input.self[i, j] = Gomer[l].population.self[i, j];
                                    Gomer[l].population.self[i, j] = 0;
                                }
                                else Gomer[l].population.self[i, j] = Gomer[l].population.self[i, j] - input.self[i, j];
                            }
                        }
                    }
                    temp.Add(input);
                }
                for (int l = 0; l < Gomer.Count; l++)
                {
                    Arrival_immigrant(Gomer[l], temp[l], Gomer[l].number);
                }
                
                Culture_assimilation(Gomer);
                //MessageBox.Show(Gomer[0].population.self[0, 0].ToString() + "\t" + Gomer[0].population.self[0, 1].ToString() + "\t" + Gomer[0].population.self[0, 2].ToString() + "\n" + Gomer[0].population.self[1, 0].ToString() + "\t" + Gomer[0].population.self[1, 1].ToString() + "\t" + Gomer[0].population.self[1, 2].ToString() + "\n" + Gomer[0].population.self[2, 0].ToString() + "\t" + Gomer[0].population.self[2, 1].ToString() + "\t" + Gomer[0].population.self[2, 2].ToString() + "\n" + Gomer[0].population.self[3, 0].ToString() + "\t" + Gomer[0].population.self[3, 1].ToString() + "\t" + Gomer[0].population.self[3, 2].ToString() + "\n" + Gomer[0].population.self[4, 0].ToString() + "\t" + Gomer[0].population.self[4, 1].ToString() + "\t" + Gomer[0].population.self[4, 2].ToString() + "\n" + Gomer[0].population.self[5, 0].ToString() + "\t" + Gomer[0].population.self[5, 1].ToString() + "\t" + Gomer[0].population.self[5, 2].ToString(), "Result " + Gomer[0].name_country, MessageBoxButtons.OK);
                //MessageBox.Show(Gomer[1].population.self[0, 0].ToString() + "\t" + Gomer[1].population.self[0, 1].ToString() + "\t" + Gomer[1].population.self[0, 2].ToString() + "\n" + Gomer[1].population.self[1, 0].ToString() + "\t" + Gomer[1].population.self[1, 1].ToString() + "\t" + Gomer[1].population.self[1, 2].ToString() + "\n" + Gomer[1].population.self[2, 0].ToString() + "\t" + Gomer[1].population.self[2, 1].ToString() + "\t" + Gomer[1].population.self[2, 2].ToString() + "\n" + Gomer[1].population.self[3, 0].ToString() + "\t" + Gomer[1].population.self[3, 1].ToString() + "\t" + Gomer[1].population.self[3, 2].ToString() + "\n" + Gomer[1].population.self[4, 0].ToString() + "\t" + Gomer[1].population.self[4, 1].ToString() + "\t" + Gomer[1].population.self[4, 2].ToString() + "\n" + Gomer[1].population.self[5, 0].ToString() + "\t" + Gomer[1].population.self[5, 1].ToString() + "\t" + Gomer[1].population.self[5, 2].ToString(), "Result " + Gomer[1].name_country, MessageBoxButtons.OK);
                //MessageBox.Show(Gomer[2].population.self[0, 0].ToString() + "\t" + Gomer[2].population.self[0, 1].ToString() + "\t" + Gomer[2].population.self[0, 2].ToString() + "\n" + Gomer[2].population.self[1, 0].ToString() + "\t" + Gomer[2].population.self[1, 1].ToString() + "\t" + Gomer[2].population.self[1, 2].ToString() + "\n" + Gomer[2].population.self[2, 0].ToString() + "\t" + Gomer[2].population.self[2, 1].ToString() + "\t" + Gomer[2].population.self[2, 2].ToString() + "\n" + Gomer[2].population.self[3, 0].ToString() + "\t" + Gomer[2].population.self[3, 1].ToString() + "\t" + Gomer[2].population.self[3, 2].ToString() + "\n" + Gomer[2].population.self[4, 0].ToString() + "\t" + Gomer[2].population.self[4, 1].ToString() + "\t" + Gomer[2].population.self[4, 2].ToString() + "\n" + Gomer[2].population.self[5, 0].ToString() + "\t" + Gomer[2].population.self[5, 1].ToString() + "\t" + Gomer[2].population.self[5, 2].ToString(), "Result " + Gomer[2].name_country, MessageBoxButtons.OK);
                this.comboBox1.Items.Clear();
                for (int i = 0; i < Gomer.Count; i++)
                {
                    this.comboBox1.Items.Add(Gomer[i].name_country);
                }

                double[] for_educ = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                double[] for_age = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                double[] for_cult = { 0.0f, 0.0f, 0.0f};
                double all_popul = 0.0f;
                for (int i = 0; i < Gomer.Count; i++)
                {
                    
                    for(int j=0;j<Gomer[i].population.size_first;j++)
                    {
                        for_age[j] = 0.0f;
                        for (int k=0;k<Gomer[i].population.size_second;k++)
                        {
                            for_age[j] += Gomer[i].population.self[j, k];
                        }
                    }
                    //MessageBox.Show(for_age.ToString(), "amt population",MessageBoxButtons.OK);
                    

                    for(int k = 0; k < Gomer[i].population.size_second; k++)
                    {
                        for_educ[k] = 0.0f;
                        for (int j = 0; j < Gomer[i].population.size_first; j++)
                        {
                            for_educ[k]+= Gomer[i].population.self[j, k];
                        }
                    }
                    //MessageBox.Show(for_age.ToString(), "amt population", MessageBoxButtons.OK);
                    

                    for (int k = 0; k < Gomer[i].population.size_second; k++)
                    {
                        for (int j = 0; j < Gomer[i].population.size_first; j++)
                        {
                            all_popul+= Gomer[i].population.self[j, k];
                        }
                    }
                    

                    for(int k=0;k<Gomer[i].culture.size_culture;k++)
                    {
                        for_cult[k] = 0.0f;
                        for (int j=0;j<Gomer[i].culture.size_age;j++)
                        {
                            for_cult[k] += Gomer[i].culture.distrib[k, j];
                        }                           
                    }

                dym[i].name_country = Gomer[i].name_country;
                dym[i].population.Add(all_popul);
                dym[i].culture.Add(for_cult);
                dym[i].educ.Add(for_educ);
                dym[i].age.Add(for_age);
                }
            }
            //MessageBox.Show(dym[0].population.Count.ToString(), "dym[0].population.Count.ToString()", MessageBoxButtons.OK);
            //MessageBox.Show(dym[1].population.Count.ToString(), "dym[1].population.Count.ToString()", MessageBoxButtons.OK);
            //MessageBox.Show(dym[2].population.Count.ToString(), "dym[2].population.Count.ToString()", MessageBoxButtons.OK);
            //MessageBox.Show(dym.Count.ToString(), "dym.Count.ToString()", MessageBoxButtons.OK);
            /*List<distribution> county = new List<distribution>();*/
            /*
            try
            {
                DialogResult rt=MessageBox.Show("Are you sure that you want run analysis?", "Attention", MessageBoxButtons.YesNo);
                if (rt == DialogResult.Yes)
                {
                    try
                    {
                        string sql = "select * from country";
                        //distribution temp = new distribution();
                        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fyodo\Desktop\diplom-master\diplom\myDiplom\myDiplom\bin\Debug\DATABASE.MDF;Integrated Security=True");
                        //C:\USERS\FYODO\DESKTOP\DIPLOM-MASTER\DIPLOM\MYDIPLOM\MYDIPLOM\DATABASE.MDF
                        //C:\Users\FLoginov\Source\Repos\diplom\diplom\myDiplom\myDiplom\Database.mdf
                        conn.Open();
                        
                        SqlCommand command = new SqlCommand(sql, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        
                        //List<string> data = new List<string>();
                        
                        while (reader.Read())
                        {
                            MessageBox.Show(reader.GetString(0)+" "+reader.GetString(1) ,"data", MessageBoxButtons.OK);
                            //data.Add(reader[0].ToString());
                        }
                        //MessageBox.Show(data[1], "", MessageBoxButtons.OK);
                        
                        reader.Close();
                        conn.Close();
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error", caption: "You have a problem with database connection", buttons: MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("You're right!", "Congratulations", MessageBoxButtons.OK);
                }
            }
            catch
            {

            }
    */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chart1.Series["Age"].Points.Clear();
            this.chart2.Series["Education"].Points.Clear();
            this.chart3.Series["Culture"].Points.Clear();
            int index = 0;
            try
            {

                for (int i = 0; i < Gomer.Count; i++)
                {
                    if (Gomer[i].name_country == this.comboBox1.SelectedItem.ToString())
                    {
                        index = i;
                    }
                }
            }
            catch
            {
                MessageBox.Show("You haven't choosed country", "Error", MessageBoxButtons.OK);
            }
            double amt = 0.0f;
            try
            {
                for (int j = 0; j < Gomer[index].population.size_first; j++)
                {
                    amt = 0.0f;
                    for (int i = 0; i < Gomer[index].population.size_second; i++)
                    {
                        amt = amt + Gomer[index].population.self[j, i];
                    }
                    this.chart1.Series["Age"].Points.AddXY((j + 1).ToString(), amt);
                }


                for (int j = 0; j < Gomer[index].population.size_second; j++)
                {
                    amt = 0.0f;
                    for (int i = 0; i < Gomer[index].population.size_first; i++)
                    {
                        amt = amt + Gomer[index].population.self[i, j];
                    }
                    this.chart2.Series["Education"].Points.AddXY((j + 1).ToString(), amt);
                }

                for (int i = 0; i < Gomer[index].culture.size_culture; i++)
                {
                    amt = 0.0f;                   
                    for (int j = 0; j < Gomer[index].culture.size_age; j++)
                    {
                        amt = amt + Gomer[index].culture.distrib[i, j];
                    }
                    this.chart3.Series["Culture"].Points.AddXY(Gomer[i].name_country,amt);
                }

            }
            catch
            {
                MessageBox.Show("You haven't choosed country","Error",MessageBoxButtons.OK);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void changeCultureEducationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_culture_education f = new Change_culture_education();
            f.Owner = this;
            f.ShowDialog();
        }

        private void buildingGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphs f = new graphs();
            f.Owner = this;
            f.ShowDialog();
        }
    }
}
