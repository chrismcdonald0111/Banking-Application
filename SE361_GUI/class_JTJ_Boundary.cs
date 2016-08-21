using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JTJ_Controller; // Controller Classes
using JTJ_Entity; // Entity Classes

namespace JTJ_Boundary
{
    public partial class mainForm : Form
    {
        CustomerController objCustomerController;
        EmployeeController objEmployeeController;
        JobController objJobController;
        InvoiceController objInvoiceController;
        PaymentController objPaymentController;
        ScheduleController objScheduleController;
        
        bool Admin = false;
        bool Payment_Manager = false;

        public void customer_Clear_All_Forms()
        {
            customerTextCNumber.Text = "";
            customerTextName.Text = "";
            customerTextAddress.Text = "";
            customerTextEmail.Text = "";
            customerTextPhone.Text = "";
        }
        public void payment_Clear_All_Forms()
        {
            paymentTextBoxPayID.Text = "";
            paymentTextBoxJobID.Text = "";
            paymentTextBoxPaymentAmount.Text = "";
        }
        public void employee_Clear_All_Forms()
        {
            employeeTextID.Text = "";
            employeeTextName.Text = "";
            employeeTextAddress.Text = "";
            employeeTextPayRate.Text = "";
        }
        public void job_Clear_All_Forms()
        {
            jobTextJNumber.Text = "";
            jobTextAddress.Text = "";
            jobTextDate.Text = "";
            jobTextSTime.Text = "";
            jobTextJLength.Text = "";
            jobTextJPrice.Text = "";
            jobTextAPaid.Text = "";
            jobTextBToPay.Text = "";
            jobCheckBoxComplete.Checked = false;
        }
        public void invoice_Clear_All_Forms()
        {
            invoiceTextBoxInvoiceID.Text = "";
            invoiceButtonOneTimeJob.Checked = false;
            invoiceButtonRegularJob.Checked = false;
            invoiceTextBoxC_Name.Text = "";
            invoiceTextBoxAddress.Text = "";
            invoiceTextBoxC_Email.Text = "";
            invoiceTextBoxC_PhoneNumber.Text = "";
            invoiceTextBoxEmployeeID.Text = "";
            invoiceTextBoxTotal_Price.Text = "";
            invoiceTextBoxC_Signature.Text = "";
        }
        public void schedule_Clear_All_Forms()
        {
            scheduleTextBoxDate.Text = "";
            scheduleTextBoxDay.Text = "";
            scheduleTextBoxInfo.Text = "";
        }

        // Initialize the Main Form and instantiate the Controller Classes
        public mainForm()
        {
            InitializeComponent();
            objCustomerController = new CustomerController();
            objEmployeeController = new EmployeeController();
            objJobController = new JobController();
            objInvoiceController = new InvoiceController();
            objPaymentController = new PaymentController();
            objScheduleController = new ScheduleController();
        }

        // Login Page
        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "Manager" && textBoxPassword.Text == "manager")
            {
                panelSignIn.Visible = false;
                tabControlMain.Visible = true;
                Admin = true;
            }
            else if (textBoxUsername.Text == "Receptionist" && textBoxPassword.Text == "receptionist")
            {
                panelSignIn.Visible = false;
                tabControlMain.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        // Customer Tab Buttons

        // Add a customer to the JSON data store
        private void customerButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int CNumber = Convert.ToInt32(customerTextCNumber.Text);

                objCustomerController.AddCustomer(CNumber, customerTextName.Text, customerTextAddress.Text, customerTextEmail.Text, customerTextPhone.Text);
                customer_Clear_All_Forms();
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid input type or no input. \nError:\n" + E);
            }
        }

        // Modify customer info
        private void customerButtonModify_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(customerSearchBox.Text);
            int CustomerNumber = Convert.ToInt32(customerTextCNumber.Text);
            objCustomerController.ModifyCustomer(CustomerNumber, customerTextName.Text, customerTextAddress.Text, customerTextEmail.Text, customerTextPhone.Text, ID);
            customer_Clear_All_Forms();
            customerSearchBox.Text = "";
        }

        // Retrieve and display information about customers from the JSON data store 
        private void customerButtonPrintAll_Click(object sender, EventArgs e)
        {
            if (customerButtonPrintAll.Text == "Print All")
            {
                customerLabelCNumber.Visible = false;
                customerTextCNumber.Visible = false;
                customerLabelName.Visible = false;
                customerTextName.Visible = false;
                customerLabelAddress.Visible = false;
                customerTextAddress.Visible = false;
                customerLabelEmail.Visible = false;
                customerTextEmail.Visible = false;
                customerLabelPhone.Visible = false;
                customerTextPhone.Visible = false;
                customerButtonAdd.Visible = false;
                customerButtonModify.Visible = false;
                customerButtonPayment.Visible = false;
                customerButtonPrintAll.BringToFront();
                customerButtonPrintAll.Text = "Back";
                customerButtonPrintAll.Location = new Point(271, 43);
                customerTextBoxData.Height = 289;
                customerTextBoxData.Visible = true;
                customerTextBoxData.Text = objCustomerController.ViewCustomer();
            }
            else
            {
                customerLabelCNumber.Visible = true;
                customerTextCNumber.Visible = true;
                customerLabelName.Visible = true;
                customerTextName.Visible = true;
                customerLabelAddress.Visible = true;
                customerTextAddress.Visible = true;
                customerLabelEmail.Visible = true;
                customerTextEmail.Visible = true;
                customerLabelPhone.Visible = true;
                customerTextPhone.Visible = true;
                customerButtonAdd.Visible = true;
                customerButtonPrintAll.Text = "Print All";
                customerButtonPrintAll.Location = new Point(104, 259);
                customerTextBoxData.Height = 219;
                customerTextBoxData.Text = "";
                customerTextBoxData.Visible = false;
            }
        }

        // Search for a customer by their ID i.e. Customer Number
        // Reuse: Search for a payment by ID i.e. Pay_ID
        private void customerSearchButton_Click(object sender, EventArgs e)
        {
            if (Payment_Manager == false)
            {
                // Customer Search
                if (customerSearchButton.Text == "Search")
                {
                    int ID = Convert.ToInt32(customerSearchBox.Text);
                    Customer found_Customer = new Customer();
                    found_Customer = objCustomerController.SearchCustomer(ID);
                    if (found_Customer.CustNumber == -1)
                    {
                        MessageBox.Show("No customers found.");
                    }
                    else
                    {
                        string CustomerNumber = Convert.ToString(found_Customer.CustNumber);
                        customerTextCNumber.Text = CustomerNumber;
                        customerTextName.Text = found_Customer.Name;
                        customerTextAddress.Text = found_Customer.Address;
                        customerTextEmail.Text = found_Customer.Email;
                        customerTextPhone.Text = found_Customer.PhoneNumber;
                        customerButtonModify.Visible = true;
                        customerButtonPayment.Visible = true;
                        if (Admin == true)
                        {
                            customerButtonDelete.Visible = true;
                        }
                        customerButtonAdd.Visible = false;
                        customerButtonPrintAll.Visible = false;
                        customerSearchButton.Text = "Back";
                    }
                }
                else if (customerSearchButton.Text == "Back")
                {
                    customer_Clear_All_Forms();
                    customerSearchBox.Text = "";
                    customerButtonModify.Visible = false;
                    customerButtonDelete.Visible = false;
                    customerButtonPayment.Visible = false;
                    customerButtonAdd.Visible = true;
                    customerButtonPrintAll.Visible = true;
                    customerSearchButton.Text = "Search";
                }
            }
            else if (Payment_Manager == true)
            {
                // Payment Search
                if (customerSearchButton.Text == "Search")
                {
                    int Pay_ID = Convert.ToInt32(customerSearchBox.Text);
                    int CustNumber = Convert.ToInt32(customerTextCNumber.Text);
                    Payment found_Payment = new Payment();
                    found_Payment = objPaymentController.SearchPayment(Pay_ID, CustNumber);
                    if (found_Payment.Pay_ID == -1)
                    {
                        MessageBox.Show("No payments found.");
                    }
                    else
                    {
                        string PayID = Convert.ToString(found_Payment.Pay_ID);
                        string JobID = Convert.ToString(found_Payment.Job_ID);
                        paymentTextBoxPayID.Text = PayID;
                        paymentTextBoxJobID.Text = JobID;
                        paymentTextBoxPaymentAmount.Text = found_Payment.Payment_Amount;
                        if (Admin == true)
                        {
                            paymentButtonCancelPayment.Visible = true;
                        }
                        paymentButtonRecordPayment.Visible = false;
                        paymentButtonViewPayments.Visible = false;
                        customerSearchButton.Text = "Back";
                    }
                }
                else if (customerSearchButton.Text == "Back")
                {
                    payment_Clear_All_Forms();
                    customerSearchBox.Text = "";
                    paymentButtonCancelPayment.Visible = false;
                    paymentButtonRecordPayment.Visible = true;
                    paymentButtonViewPayments.Visible = true;
                    customerSearchButton.Text = "Search";
                }
            }
        }
       
        // Delete a customer
        private void customerButtonDelete_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(customerTextCNumber.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to delete the customer?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                objCustomerController.DeleteCustomer(ID);
                customer_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No) { }
            customerSearchBox.Text = "";
        }

        // Payment Manager

        // Display the payment manager for a specific customer
        private void customerButtonPayment_Click(object sender, EventArgs e)
        {
            if(customerButtonPayment.Text == "Payment Manager")
            {
                customerSearchBox.Text = "";
                customerSearchButton.Text = "Search";
                Payment_Manager = true;
                paymentTextBoxData.Visible = true;
                paymentLabelPayID.BringToFront();
                paymentLabelJobID.BringToFront();
                paymentLabelPaymentAmount.BringToFront();
                paymentTextBoxPayID.BringToFront();
                paymentTextBoxJobID.BringToFront();
                paymentTextBoxPaymentAmount.BringToFront();
                paymentLabelPayID.Visible = true;
                paymentLabelJobID.Visible = true;
                paymentLabelPaymentAmount.Visible = true;
                paymentTextBoxPayID.Visible = true;
                paymentTextBoxJobID.Visible = true;
                paymentTextBoxPaymentAmount.Visible = true;
                paymentButtonRecordPayment.Visible = true;
                paymentButtonViewPayments.Visible = true;
                customerButtonPayment.Text = "Back";
            }
            else if (customerButtonPayment.Text == "Back")
            {
                customerSearchBox.Text = "";
                customerSearchButton.Text = "Back";
                Payment_Manager = false;
                paymentTextBoxData.Visible = false;
                paymentLabelPayID.Visible = false;
                paymentLabelJobID.Visible = false;
                paymentLabelPaymentAmount.Visible = false;
                paymentTextBoxPayID.Visible = false;
                paymentTextBoxJobID.Visible = false;
                paymentTextBoxPaymentAmount.Visible = false;
                paymentButtonRecordPayment.Visible = false;
                paymentButtonViewPayments.Visible = false;
                paymentButtonCancelPayment.Visible = false;
                customerButtonPayment.Text = "Payment Manager";
                payment_Clear_All_Forms();
            }
        }

        // Record a payment for a specific customer
        private void paymentButtonRecordPayment_Click(object sender, EventArgs e)
        {
            try
            {
                int PayID = Convert.ToInt32(paymentTextBoxPayID.Text);
                int CustNumber = Convert.ToInt32(customerTextCNumber.Text);
                int JobID = Convert.ToInt32(paymentTextBoxJobID.Text);

                objPaymentController.RecordPayment(PayID, CustNumber, JobID, paymentTextBoxPaymentAmount.Text);
                payment_Clear_All_Forms();
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid input type or no input. \nError:\n" + E);
            }
        }

        // Print all of the payments for a specific customer
        private void paymentButtonViewPayments_Click(object sender, EventArgs e)
        {
            if (paymentButtonViewPayments.Text == "Print")
            {
                int CustNumber = Convert.ToInt32(customerTextCNumber.Text);
                paymentButtonRecordPayment.Visible = false;
                paymentLabelPayID.Visible = false;
                paymentLabelJobID.Visible = false;
                paymentLabelPaymentAmount.Visible = false;
                paymentTextBoxPayID.Visible = false;
                paymentTextBoxJobID.Visible = false;
                paymentTextBoxPaymentAmount.Visible = false;
                paymentButtonViewPayments.BringToFront();
                paymentButtonViewPayments.Text = "Back";
                paymentButtonViewPayments.Location = new Point(271, 43);
                paymentTextBoxData.Height = 289;
                paymentTextBoxData.Text = objPaymentController.ViewPayment(CustNumber);
            }
            else if (paymentButtonViewPayments.Text == "Back")
            {
                paymentButtonRecordPayment.Visible = true;
                paymentLabelPayID.Visible = true;
                paymentLabelJobID.Visible = true;
                paymentLabelPaymentAmount.Visible = true;
                paymentTextBoxPayID.Visible = true;
                paymentTextBoxJobID.Visible = true;
                paymentTextBoxPaymentAmount.Visible = true;
                paymentButtonViewPayments.Text = "Print";
                paymentButtonViewPayments.Location = new Point(88, 298);
                paymentTextBoxData.Height = 259;
                paymentTextBoxData.Text = "";
            }
        }

        // Cancel a payment for a specific customer
        private void paymentButtonCancelPayment_Click(object sender, EventArgs e)
        {
            int PayID = Convert.ToInt32(paymentTextBoxPayID.Text);
            int CustNumber = Convert.ToInt32(customerTextCNumber.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to cancel the payment?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                objPaymentController.CancelPayment(PayID, CustNumber);
                payment_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No) { }
            customerSearchBox.Text = "";
        }     

        // Employee Tab Buttons

        // Add an employee to the JSON data store
        private void employeeButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int EmpID = Convert.ToInt32(employeeTextID.Text);

                objEmployeeController.AddEmployee(EmpID, employeeTextName.Text, employeeTextAddress.Text, employeeTextPayRate.Text);
                employee_Clear_All_Forms();
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid input type or no input. \nError:\n" + E);
            }
        }

        // Modify employee info
        private void employeeButtonModify_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(employeeSearchBox.Text);
            int EmployeeID = Convert.ToInt32(employeeTextID.Text);
            objEmployeeController.ModifyEmployee(EmployeeID, employeeTextName.Text, employeeTextAddress.Text, employeeTextPayRate.Text, ID);
            employee_Clear_All_Forms();
            employeeSearchBox.Text = "";
        }

        // Retrieve and display information about employees from the JSON data store
        private void employeeButtonPrintAll_Click(object sender, EventArgs e)
        {
            if (employeeButtonPrintAll.Text == "Print All")
            {
                employeeLabelID.Visible = false;
                employeeTextID.Visible = false;
                employeeLabelName.Visible = false;
                employeeTextName.Visible = false;
                employeeLabelAddress.Visible = false;
                employeeTextAddress.Visible = false;
                employeeLabelPayRate.Visible = false;
                employeeTextPayRate.Visible = false;
                employeeButtonAdd.Visible = false;
                employeeButtonPrintAll.BringToFront();
                employeeButtonPrintAll.Text = "Back";
                employeeButtonPrintAll.Location = new Point(271, 43);
                employeeTextBoxData.Height = 289;
                employeeTextBoxData.Visible = true;
                employeeTextBoxData.Text = objEmployeeController.ViewEmployee();
            }
            else
            {
                employeeLabelID.Visible = true;
                employeeTextID.Visible = true;
                employeeLabelName.Visible = true;
                employeeTextName.Visible = true;
                employeeLabelAddress.Visible = true;
                employeeTextAddress.Visible = true;
                employeeLabelPayRate.Visible = true;
                employeeTextPayRate.Visible = true;
                employeeButtonAdd.Visible = true;
                employeeButtonPrintAll.Text = "Print All";
                employeeButtonPrintAll.Location = new Point(104, 259);
                employeeTextBoxData.Height = 219;
                employeeTextBoxData.Text = "";
                employeeTextBoxData.Visible = false;
            }

        }

        // Search for an employee by their ID
        private void employeeButtonSearch_Click(object sender, EventArgs e)
        {
            if (employeeButtonSearch.Text == "Search")
            {
                int ID = Convert.ToInt32(employeeSearchBox.Text);
                Employee found_Employee = new Employee();
                found_Employee = objEmployeeController.SearchEmployee(ID);
                if (found_Employee.ID == -1)
                {
                    MessageBox.Show("No employees found.");
                }
                else
                {
                    string EmployeeID = Convert.ToString(found_Employee.ID);
                    employeeTextID.Text = EmployeeID;
                    employeeTextName.Text = found_Employee.Name;
                    employeeTextAddress.Text = found_Employee.Address;
                    employeeTextPayRate.Text = found_Employee.Pay_Rate;
                    employeeButtonModify.Visible = true;
                    if (Admin == true)
                    {
                        employeeButtonDelete.Visible = true;
                    }
                    employeeButtonAdd.Visible = false;
                    employeeButtonPrintAll.Visible = false;
                    employeeButtonSearch.Text = "Back";
                }
            }
            else if (employeeButtonSearch.Text == "Back")
            {
                employee_Clear_All_Forms();
                employeeSearchBox.Text = "";
                employeeButtonModify.Visible = false;
                employeeButtonDelete.Visible = false;
                employeeButtonAdd.Visible = true;
                employeeButtonPrintAll.Visible = true;
                employeeButtonSearch.Text = "Search";
            }
        }

        // Delete an employee
        private void employeeButtonDelete_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(employeeTextID.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to delete the employee?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                objEmployeeController.DeleteEmployee(ID);
                employee_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No) { }
            employeeSearchBox.Text = "";
        }

        // Job Tab Buttons

        // Add a job to the JSON data store
        private void jobButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int JobNumber = Convert.ToInt32(jobTextJNumber.Text);

                objJobController.AddJob(JobNumber, jobCheckBoxComplete.Checked, jobTextAddress.Text, jobTextDate.Text, jobTextSTime.Text, jobTextJLength.Text, jobTextJPrice.Text, jobTextAPaid.Text, jobTextBToPay.Text);
                job_Clear_All_Forms();
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid input type or no input. \nError:\n" + E);
            }
        }

        // Modify job info
        private void jobButtonModify_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(jobSearchBox.Text);
            int JobNumber = Convert.ToInt32(jobTextJNumber.Text);
            objJobController.ModifyJob(JobNumber, jobCheckBoxComplete.Checked, jobTextAddress.Text, jobTextDate.Text, jobTextSTime.Text, jobTextJLength.Text, jobTextJPrice.Text, jobTextAPaid.Text, jobTextBToPay.Text, ID);
            job_Clear_All_Forms();
            jobSearchBox.Text = "";
        }

        // Retrieve and display information about jobs from the JSON data store
        private void jobButtonPrintAll_Click(object sender, EventArgs e)
        {
            if (jobButtonPrintAll.Text == "Print All")
            {
                jobLabelJNumber.Visible = false;
                jobTextJNumber.Visible = false;
                jobLabelAddress.Visible = false;
                jobTextAddress.Visible = false;
                jobLabelDate.Visible = false;
                jobTextDate.Visible = false;
                jobLabelSTime.Visible = false;
                jobTextSTime.Visible = false;
                jobCheckBoxComplete.Visible = false;
                jobLabelJLength.Visible = false;
                jobTextJLength.Visible = false;
                jobLabelJPrice.Visible = false;
                jobTextJPrice.Visible = false;
                jobLabelAPaid.Visible = false;
                jobTextAPaid.Visible = false;
                jobLabelBToPay.Visible = false;
                jobTextBToPay.Visible = false;
                jobDropDownStatus.Visible = true;
                jobButtonAdd.Visible = false;
                jobButtonPrintAll.BringToFront();
                jobButtonPrintAll.Text = "Back";
                jobButtonPrintAll.Location = new Point(271, 45);
                jobTextBoxData.Height = 289;
                jobTextBoxData.Visible = true;
                jobTextBoxData.Text = objJobController.ViewJob();
            }
            else
            {
                jobLabelJNumber.Visible = true;
                jobTextJNumber.Visible = true;
                jobLabelAddress.Visible = true;
                jobTextAddress.Visible = true;
                jobLabelDate.Visible = true;
                jobTextDate.Visible = true;
                jobLabelSTime.Visible = true;
                jobTextSTime.Visible = true;
                jobCheckBoxComplete.Visible = true;
                jobLabelJLength.Visible = true;
                jobTextJLength.Visible = true;
                jobLabelJPrice.Visible = true;
                jobTextJPrice.Visible = true;
                jobLabelAPaid.Visible = true;
                jobTextAPaid.Visible = true;
                jobLabelBToPay.Visible = true;
                jobTextBToPay.Visible = true;
                jobDropDownStatus.Visible = false;
                jobButtonAdd.Visible = true;
                jobButtonPrintAll.Text = "Print All";
                jobButtonPrintAll.Location = new Point(104, 266);
                jobTextBoxData.Height = 219;
                jobTextBoxData.Text = "";
                jobTextBoxData.Visible = false;
            }
        }

        // View jobs based on Completion Status
        private void jobDropDownStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(jobDropDownStatus.Text == "Completed")
            {
                jobTextBoxData.Text = objJobController.ViewJobByStatus(true);
            }
            else if(jobDropDownStatus.Text == "In Progress")
            {
                jobTextBoxData.Text = objJobController.ViewJobByStatus(false);
            }
            else
            {
                jobTextBoxData.Text = objJobController.ViewJob();
            }
            
        }   

        // Search for a job by ID i.e. Job Number
        private void jobButtonSearch_Click(object sender, EventArgs e)
        {
            if (jobButtonSearch.Text == "Search")
            {
                int ID = Convert.ToInt32(jobSearchBox.Text);
                Job found_Job = new Job();
                found_Job = objJobController.SearchJob(ID);
                if (found_Job.JobNumber == -1)
                {
                    MessageBox.Show("No jobs found.");
                }
                else
                {
                    string JobNumber = Convert.ToString(found_Job.JobNumber);
                    jobTextJNumber.Text = JobNumber;
                    jobTextAddress.Text = found_Job.Address;
                    jobTextDate.Text = found_Job.Date;
                    jobTextSTime.Text = found_Job.Start_Time;
                    jobTextJLength.Text = found_Job.Length;
                    jobTextJPrice.Text = found_Job.Price;
                    jobTextAPaid.Text = found_Job.Amount_Paid;
                    jobTextBToPay.Text = found_Job.Amount_ToPay;
                    jobCheckBoxComplete.Checked = found_Job.Complete;
                    jobButtonModify.Visible = true;
                    if (Admin == true)
                    {
                        jobButtonDelete.Visible = true;
                    }
                    jobButtonAdd.Visible = false;
                    jobButtonPrintAll.Visible = false;
                    jobButtonSearch.Text = "Back";
                }
            }
            else if (jobButtonSearch.Text == "Back")
            {
                job_Clear_All_Forms();
                jobSearchBox.Text = "";
                jobButtonModify.Visible = false;
                jobButtonDelete.Visible = false;
                jobButtonAdd.Visible = true;
                jobButtonPrintAll.Visible = true;
                jobButtonSearch.Text = "Search";
            }
        }

        // Delete a job
        private void jobButtonDelete_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(jobTextJNumber.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to delete the job?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                objJobController.DeleteJob(ID);
                job_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No) { }
            jobSearchBox.Text = "";
        }

        // Invoice Tab Buttons

        // Add an invoice
        private void invoiceButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int invoice_ID = Convert.ToInt32(invoiceTextBoxInvoiceID.Text);
                if (invoiceButtonOneTimeJob.Checked == true)
                {
                    objInvoiceController.CreateInvoice(invoice_ID, true, invoiceTextBoxC_Name.Text, invoiceTextBoxAddress.Text, invoiceTextBoxC_Email.Text, invoiceTextBoxC_PhoneNumber.Text, invoiceTextBoxEmployeeID.Text, invoiceTextBoxTotal_Price.Text, invoiceTextBoxC_Signature.Text);
                    invoice_Clear_All_Forms();
                }
                else if (invoiceButtonRegularJob.Checked == true)
                {
                    objInvoiceController.CreateInvoice(invoice_ID, false, invoiceTextBoxC_Name.Text, invoiceTextBoxAddress.Text, invoiceTextBoxC_Email.Text, invoiceTextBoxC_PhoneNumber.Text, invoiceTextBoxEmployeeID.Text, invoiceTextBoxTotal_Price.Text, invoiceTextBoxC_Signature.Text);
                    invoice_Clear_All_Forms();
                }
                else if (invoiceButtonOneTimeJob.Checked == false && invoiceButtonRegularJob.Checked == false)
                {
                    MessageBox.Show("No Job Type selected.");
                }
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid input type or no input. \nError:\n" + E);
            }
        }

        // Modify an invoice
        private void invoiceButtonModify_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(invoiceSearchBox.Text);
            int invoice_ID = Convert.ToInt32(invoiceTextBoxInvoiceID.Text);
            if (invoiceButtonOneTimeJob.Checked == true)
            {
                objInvoiceController.ModifyInvoice(invoice_ID, true, invoiceTextBoxC_Name.Text, invoiceTextBoxAddress.Text, invoiceTextBoxC_Email.Text, invoiceTextBoxC_PhoneNumber.Text, invoiceTextBoxEmployeeID.Text, invoiceTextBoxTotal_Price.Text, invoiceTextBoxC_Signature.Text, ID);
                invoice_Clear_All_Forms();
            }
            else if (invoiceButtonRegularJob.Checked == true)
            {
                objInvoiceController.ModifyInvoice(invoice_ID, false, invoiceTextBoxC_Name.Text, invoiceTextBoxAddress.Text, invoiceTextBoxC_Email.Text, invoiceTextBoxC_PhoneNumber.Text, invoiceTextBoxEmployeeID.Text, invoiceTextBoxTotal_Price.Text, invoiceTextBoxC_Signature.Text, ID);
                invoice_Clear_All_Forms();
            }
            else if (invoiceButtonOneTimeJob.Checked == false && invoiceButtonRegularJob.Checked == false)
            {
                MessageBox.Show("No Job Type selected.");
            }
            invoiceSearchBox.Text = "";
        }

        // Display all invoices
        private void invoiceButtonPrintAll_Click(object sender, EventArgs e)
        {
            if (invoiceButtonPrintAll.Text == "Print All")
            {
                invoiceLabelInvoiceID.Visible = false;
                invoiceTextBoxInvoiceID.Visible = false;
                invoiceLabelC_Name.Visible = false;
                invoiceTextBoxC_Name.Visible = false;
                invoiceLabelAddress.Visible = false;
                invoiceTextBoxAddress.Visible = false;
                invoiceLabelC_Email.Visible = false;
                invoiceTextBoxC_Email.Visible = false;
                invoiceLabelC_PhoneNumber.Visible = false;
                invoiceTextBoxC_PhoneNumber.Visible = false;
                invoiceLabelEmployeeID.Visible = false;
                invoiceTextBoxEmployeeID.Visible = false;
                invoiceLabelTotal_Price.Visible = false;
                invoiceTextBoxTotal_Price.Visible = false;
                invoiceLabelC_Signature.Visible = false;
                invoiceTextBoxC_Signature.Visible = false; 
                invoiceButtonCreate.Visible = false;
                invoiceButtonModify.Visible = false;
                invoiceButtonPrintAll.BringToFront();
                invoiceButtonPrintAll.Text = "Back";
                invoiceButtonPrintAll.Location = new Point(267, 67);
                invoiceTextBoxData.Height = 279;
                invoiceTextBoxData.Visible = true;
                if (invoiceButtonOneTimeJob.Checked == true)
                {
                    invoiceTextBoxData.Text = objInvoiceController.ViewInvoiceByJobType(true);
                    invoiceButtonOneTimeJob.Checked = false;
                    invoiceButtonRegularJob.Checked = false;
                }
                else if (invoiceButtonRegularJob.Checked == true)
                {
                    invoiceTextBoxData.Text = objInvoiceController.ViewInvoiceByJobType(false);
                    invoiceButtonOneTimeJob.Checked = false;
                    invoiceButtonRegularJob.Checked = false;
                }
                else if (invoiceButtonOneTimeJob.Checked == false && invoiceButtonRegularJob.Checked == false)
                {
                    invoiceTextBoxData.Text = objInvoiceController.ViewInvoice();
                    invoiceButtonOneTimeJob.Checked = false;
                    invoiceButtonRegularJob.Checked = false;
                }
                
            }
            else
            {
                invoiceLabelInvoiceID.Visible = true;
                invoiceTextBoxInvoiceID.Visible = true;
                invoiceLabelC_Name.Visible = true;
                invoiceTextBoxC_Name.Visible = true;
                invoiceLabelAddress.Visible = true;
                invoiceTextBoxAddress.Visible = true;
                invoiceLabelC_Email.Visible = true;
                invoiceTextBoxC_Email.Visible = true;
                invoiceLabelC_PhoneNumber.Visible = true;
                invoiceTextBoxC_PhoneNumber.Visible = true;
                invoiceLabelEmployeeID.Visible = true;
                invoiceTextBoxEmployeeID.Visible = true;
                invoiceLabelTotal_Price.Visible = true;
                invoiceTextBoxTotal_Price.Visible = true;
                invoiceLabelC_Signature.Visible = true;
                invoiceTextBoxC_Signature.Visible = true;
                invoiceButtonCreate.Visible = true;
                invoiceButtonPrintAll.Text = "Print All";
                invoiceButtonPrintAll.Location = new Point(102, 273);
                invoiceTextBoxData.Height = 219;
                invoiceTextBoxData.Text = "";
                invoiceTextBoxData.Visible = false;
            }
        }

        // Search for an invoice by ID
        private void invoiceButtonSearch_Click(object sender, EventArgs e)
        {
            if (invoiceButtonSearch.Text == "Search")
            {
                int ID = Convert.ToInt32(invoiceSearchBox.Text);
                Invoice found_Invoice = new Invoice();
                found_Invoice = objInvoiceController.SearchInvoice(ID);
                if (found_Invoice.invoice_ID == -1)
                {
                    MessageBox.Show("No employees found.");
                }
                else
                {
                    string invoiceID = Convert.ToString(found_Invoice.invoice_ID);
                    invoiceTextBoxInvoiceID.Text = invoiceID;
                    if(found_Invoice.Job_Type == true){
                        invoiceButtonOneTimeJob.Checked = true;
                    }
                    else if (found_Invoice.Job_Type == false)
                    {
                        invoiceButtonRegularJob.Checked = true;
                    }
                    invoiceTextBoxC_Name.Text = found_Invoice.C_Name;
                    invoiceTextBoxAddress.Text = found_Invoice.Address;
                    invoiceTextBoxC_Email.Text = found_Invoice.C_Email;
                    invoiceTextBoxC_PhoneNumber.Text = found_Invoice.C_PhoneNumber;
                    invoiceTextBoxEmployeeID.Text = found_Invoice.Employee_ID;
                    invoiceTextBoxTotal_Price.Text = found_Invoice.Total_Price;
                    invoiceTextBoxC_Signature.Text = found_Invoice.C_Signature;
                    invoiceButtonModify.Visible = true;
                    if (Admin == true)
                    {
                        invoiceButtonCancel.Visible = true;
                    }
                    invoiceButtonCreate.Visible = false;
                    invoiceButtonPrintAll.Visible = false;
                    invoiceButtonSearch.Text = "Back";
                }
            }
            else if (invoiceButtonSearch.Text == "Back")
            {
                invoice_Clear_All_Forms();
                invoiceSearchBox.Text = "";
                invoiceButtonModify.Visible = false;
                invoiceButtonCancel.Visible = false;
                invoiceButtonCreate.Visible = true;
                invoiceButtonPrintAll.Visible = true;
                invoiceButtonSearch.Text = "Search";
            }
        }

        // Cancel an invoice
        private void invoiceButtonCancel_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(invoiceTextBoxInvoiceID.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to cancel the invoice?", "Confirm Cancellation", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                objInvoiceController.CancelInvoice(ID);
                invoice_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No){ }
            invoiceSearchBox.Text = "";
        }

        // Schedule Tab Buttons

        // Select a date from the calendar
        private void scheduleCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            scheduleCalendar.Visible = false;
            scheduleButtonAdd.Visible = true;
            scheduleButtonAdd.BringToFront();
            scheduleButtonPrint.Visible = true;
            scheduleButtonModify.Visible = true;
            scheduleButtonCancel.Visible = true;
            scheduleButtonBack.Visible = true;
            scheduleLabelDate.Visible = true;
            scheduleLabelDay.Visible = true;
            scheduleLabelInfo.Visible = true;
            scheduleTextBoxDate.Visible = true;
            scheduleTextBoxDay.Visible = true;
            scheduleTextBoxInfo.Visible = true;
            string Date = scheduleCalendar.SelectionStart.ToShortDateString();
            string Day = scheduleCalendar.SelectionStart.DayOfWeek.ToString();
            scheduleTextBoxDate.Text = Date;
            scheduleTextBoxDay.Text = Day;
            Schedule found_Schedule = objScheduleController.SearchDate(Date);
            scheduleTextBoxInfo.Text = found_Schedule.Info;   
        }

        // Add an event for a specific date on the calendar
        private void scheduleButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                objScheduleController.AddSchedule(scheduleTextBoxDate.Text, scheduleTextBoxDay.Text, scheduleTextBoxInfo.Text);
                schedule_Clear_All_Forms();
            }
            catch (FormatException E)
            {
                MessageBox.Show("Invalid or no input.\nError:\n" + E);
            }
        }

        // Modify information for a specific date on the calendar
        private void scheduleButtonModify_Click(object sender, EventArgs e)
        {
            objScheduleController.ModifySchedule(scheduleTextBoxDate.Text, scheduleTextBoxDay.Text, scheduleTextBoxInfo.Text);
            schedule_Clear_All_Forms();
        }

        // Print a one week block from the calendar based on the selected date
        private void scheduleButtonPrint_Click(object sender, EventArgs e)
        {
            if (scheduleButtonPrint.Text == "Print")
            {
                scheduleTextBoxData.Visible = true;
                scheduleTextBoxData.BringToFront();
                scheduleButtonBack.Visible = false;
                scheduleButtonAdd.Visible = false;
                scheduleButtonModify.Visible = false;
                scheduleButtonCancel.Visible = false;
                scheduleButtonPrint.Text = "Back";
                scheduleButtonPrint.Location = new Point(291, 7);
                scheduleTextBoxData.Height = 289;
                scheduleButtonPrint.BringToFront();
                string Date = scheduleCalendar.SelectionStart.ToShortDateString();
                scheduleTextBoxData.Text = objScheduleController.ViewOnewWeekSchedule(Date);
            }
            else if (scheduleButtonPrint.Text == "Back")
            {
                scheduleTextBoxData.Visible = false;
                scheduleTextBoxData.SendToBack();
                scheduleButtonBack.Visible = true;
                scheduleButtonAdd.Visible = true;
                scheduleButtonModify.Visible = true;
                scheduleButtonCancel.Visible = true;
                scheduleButtonPrint.Text = "Print";
                scheduleButtonPrint.Location = new Point(104, 299);
                scheduleTextBoxData.Height = 259;
            }
        }

        // Cancel an event for a specific date from the calendar
        private void scheduleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show("Are you sure you wish to cancel this date's events?", "Confirm Cancellation", MessageBoxButtons.YesNo);
            if (confirmation == DialogResult.Yes)
            {
                string Date = scheduleCalendar.SelectionStart.ToShortDateString();
                objScheduleController.CancelDate(Date);
                schedule_Clear_All_Forms();
            }
            else if (confirmation == DialogResult.No) { }
        }

        // Go back to the calendar
        private void scheduleButtonBack_Click(object sender, EventArgs e)
        {
            scheduleCalendar.Visible = true;
            scheduleButtonAdd.Visible = false;
            scheduleButtonPrint.Visible = false;
            scheduleButtonModify.Visible = false;
            scheduleButtonCancel.Visible = false;
            scheduleButtonBack.Visible = false;
            scheduleLabelDate.Visible = false;
            scheduleLabelDay.Visible = false;
            scheduleLabelInfo.Visible = false;
            scheduleTextBoxDate.Visible = false;
            scheduleTextBoxDay.Visible = false;
            scheduleTextBoxInfo.Visible = false;
            scheduleTextBoxData.Visible = false;
        }

        // Misc. Ignore

        // Add validation here eventually 
        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
        private void customerTextBoxData_TextChanged(object sender, EventArgs e)
        {

        }
        private void customerSearchBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void employeeSearchBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void jobSearchBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void invoiceButtonOneTimeJob_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void invoiceButtonRegularJob_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
