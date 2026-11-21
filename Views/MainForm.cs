using System.Data;
using CarRentalSystem.Controllers;
namespace CarRentalSystem.Views;
public class MainForm:Form {
 readonly ManagementController c=new(); readonly TabControl tabs=new(){Dock=DockStyle.Fill};
 readonly DataGridView cg=Grid(),ag=Grid(),bg=Grid(),pg=Grid();
 readonly TextBox cn=new(),ce=new(),cp=new(),reg=new(),make=new(),model=new(),cat=new();
 readonly NumericUpDown year=Num(1980,2100,2022),rate=Num(0,10000000,0,2),total=Num(0,100000000,0,2),amount=Num(0,100000000,0,2);
 readonly ComboBox carState=Pick("Available","Booked","Rented","Maintenance"),bc=new(),ba=new(),bs=Pick("Pending","Approved","Cancelled","Completed"),pb=new(),pm=Pick("Cash","Card","Mobile Money","Bank Transfer"),ps=Pick("Pending","Paid","Failed","Refunded");
 readonly DateTimePicker sd=new(),ed=new(),pd=new();
 public MainForm(){Text="Car Rental Management System";Width=1200;Height=760;StartPosition=FormStartPosition.CenterScreen;Controls.Add(tabs);CustomersTab();CarsTab();BookingsTab();PaymentsTab();tabs.SelectedIndexChanged+=(_,_)=>RefreshTab();Shown+=(_,_)=>RefreshTab();}
 static DataGridView Grid()=>new(){Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false};
 static NumericUpDown Num(decimal min,decimal max,decimal val,int decimals=0)=>new(){Minimum=min,Maximum=max,Value=val,DecimalPlaces=decimals,Width=130};
 static ComboBox Pick(params string[] items){var x=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList,Width=150};x.Items.AddRange(items);x.SelectedIndex=0;return x;}
 static Label Lab(string s)=>new(){Text=s,AutoSize=true,Margin=new Padding(7,10,2,2)};
 static FlowLayoutPanel Panel()=>new(){Dock=DockStyle.Top,AutoSize=true,Padding=new Padding(6),WrapContents=true};
 Button Btn(string s,Action a){var b=new Button{Text=s,AutoSize=true,Height=30,Margin=new Padding(5)};b.Click+=(_,_)=>Safe(a);return b;}
 void Safe(Action a){try{a();}catch(Exception ex){MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
 int Id(DataGridView g)=>Convert.ToInt32(g.CurrentRow?.Cells["ID"].Value??throw new Exception("Select a row first."));
 bool Confirm()=>MessageBox.Show("Delete selected record?","Confirm",MessageBoxButtons.YesNo)==DialogResult.Yes;
 void CustomersTab(){var t=new TabPage("Customers");var p=Panel();p.Controls.AddRange(new Control[]{Lab("Full name"),cn,Lab("Email"),ce,Lab("Phone"),cp,Btn("Add",()=>{c.AddCustomer(cn.Text,ce.Text,cp.Text);LoadC();}),Btn("Update",()=>{c.UpdateCustomer(Id(cg),cn.Text,ce.Text,cp.Text);LoadC();}),Btn("Delete",()=>{if(Confirm())c.DeleteCustomer(Id(cg));LoadC();})});cg.SelectionChanged+=(_,_)=>{if(cg.CurrentRow!=null){cn.Text=V(cg,"Full name");ce.Text=V(cg,"Email");cp.Text=V(cg,"Phone");}};t.Controls.Add(cg);t.Controls.Add(p);tabs.TabPages.Add(t);}
 void CarsTab(){var t=new TabPage("Cars");var p=Panel();p.Controls.AddRange(new Control[]{Lab("Registration"),reg,Lab("Make"),make,Lab("Model"),model,Lab("Year"),year,Lab("Category"),cat,Lab("Daily rate"),rate,Lab("Status"),carState,Btn("Add",()=>{c.AddCar(reg.Text,make.Text,model.Text,(int)year.Value,cat.Text,rate.Value,carState.Text);LoadA();}),Btn("Update",()=>{c.UpdateCar(Id(ag),reg.Text,make.Text,model.Text,(int)year.Value,cat.Text,rate.Value,carState.Text);LoadA();}),Btn("Delete",()=>{if(Confirm())c.DeleteCar(Id(ag));LoadA();})});ag.SelectionChanged+=(_,_)=>{if(ag.CurrentRow!=null){reg.Text=V(ag,"Registration");make.Text=V(ag,"Make");model.Text=V(ag,"Model");cat.Text=V(ag,"Category");int.TryParse(V(ag,"Year"),out int y);year.Value=Math.Clamp(y,(int)year.Minimum,(int)year.Maximum);decimal.TryParse(V(ag,"Daily rate"),out decimal r);rate.Value=Math.Clamp(r,rate.Minimum,rate.Maximum);carState.Text=V(ag,"Status");}};t.Controls.Add(ag);t.Controls.Add(p);tabs.TabPages.Add(t);}
 void BookingsTab(){var t=new TabPage("Bookings");var p=Panel();p.Controls.AddRange(new Control[]{Lab("Customer"),bc,Lab("Available car"),ba,Lab("Start"),sd,Lab("End"),ed,Lab("Total"),total,Btn("Create booking",()=>{c.AddBooking(Cid(bc),Cid(ba),sd.Value,ed.Value,total.Value);LoadB();Choices();}),Lab("Selected status"),bs,Btn("Update status",()=>{c.BookingStatus(Id(bg),bs.Text);LoadB();}),Btn("Delete",()=>{if(Confirm())c.DeleteBooking(Id(bg));LoadB();Choices();})});bg.SelectionChanged+=(_,_)=>{if(bg.CurrentRow!=null)bs.Text=V(bg,"Status");};t.Controls.Add(bg);t.Controls.Add(p);tabs.TabPages.Add(t);}
 void PaymentsTab(){var t=new TabPage("Payments");var p=Panel();p.Controls.AddRange(new Control[]{Lab("Booking"),pb,Lab("Amount"),amount,Lab("Date"),pd,Lab("Method"),pm,Lab("Status"),ps,Btn("Record payment",()=>{c.AddPayment(Cid(pb),amount.Value,pd.Value,pm.Text,ps.Text);LoadP();}),Btn("Update status",()=>{c.PaymentStatus(Id(pg),ps.Text);LoadP();}),Btn("Delete",()=>{if(Confirm())c.DeletePayment(Id(pg));LoadP();})});pg.SelectionChanged+=(_,_)=>{if(pg.CurrentRow!=null)ps.Text=V(pg,"Status");};t.Controls.Add(pg);t.Controls.Add(p);tabs.TabPages.Add(t);}
 static string V(DataGridView g,string col)=>Convert.ToString(g.CurrentRow!.Cells[col].Value)??"";
 static int Cid(ComboBox b)=>Convert.ToInt32(b.SelectedValue??throw new Exception("Select an item."));
 void LoadC()=>cg.DataSource=c.Customers();void LoadA()=>ag.DataSource=c.Cars();void LoadB()=>bg.DataSource=c.Bookings();void LoadP()=>pg.DataSource=c.Payments();
 void Choices(){bc.DataSource=c.BookingCustomers();bc.DisplayMember="full_name";bc.ValueMember="customer_id";ba.DataSource=c.AvailableCars();ba.DisplayMember="car_name";ba.ValueMember="car_id";}
 void PaymentChoices(){pb.DataSource=c.PaymentBookings();pb.DisplayMember="booking_id";pb.ValueMember="booking_id";}
 void RefreshTab()=>Safe(()=>{switch(tabs.SelectedIndex){case 0:LoadC();break;case 1:LoadA();break;case 2:LoadB();Choices();break;case 3:LoadP();PaymentChoices();break;}});
}