using System.Data;
using CarRentalSystem.Data;
namespace CarRentalSystem.Models;
public class CustomerRepository {
 public DataTable All()=>Db.Query("SELECT customer_id AS ID,full_name AS `Full name`,email AS Email,phone AS Phone FROM customers ORDER BY customer_id DESC");
 public void Add(string n,string e,string p)=>Db.Execute("INSERT INTO customers(full_name,email,phone) VALUES(@n,@e,@p)",Db.P("@n",n),Db.P("@e",e),Db.P("@p",p));
 public void Update(int id,string n,string e,string p)=>Db.Execute("UPDATE customers SET full_name=@n,email=@e,phone=@p WHERE customer_id=@id",Db.P("@n",n),Db.P("@e",e),Db.P("@p",p),Db.P("@id",id));
 public void Delete(int id)=>Db.Execute("DELETE FROM customers WHERE customer_id=@id",Db.P("@id",id));
}
public class CarRepository {
 public DataTable All()=>Db.Query("SELECT car_id AS ID,registration_number AS Registration,make AS Make,model AS Model,year AS Year,category AS Category,daily_rate AS `Daily rate`,status AS Status FROM cars ORDER BY car_id DESC");
 public void Add(string r,string ma,string mo,int y,string cat,decimal rate,string s)=>Db.Execute("INSERT INTO cars(registration_number,make,model,year,category,daily_rate,status) VALUES(@r,@ma,@mo,@y,@cat,@rate,@s)",Db.P("@r",r),Db.P("@ma",ma),Db.P("@mo",mo),Db.P("@y",y),Db.P("@cat",cat),Db.P("@rate",rate),Db.P("@s",s));
 public void Update(int id,string r,string ma,string mo,int y,string cat,decimal rate,string s)=>Db.Execute("UPDATE cars SET registration_number=@r,make=@ma,model=@mo,year=@y,category=@cat,daily_rate=@rate,status=@s WHERE car_id=@id",Db.P("@r",r),Db.P("@ma",ma),Db.P("@mo",mo),Db.P("@y",y),Db.P("@cat",cat),Db.P("@rate",rate),Db.P("@s",s),Db.P("@id",id));
 public void Delete(int id)=>Db.Execute("DELETE FROM cars WHERE car_id=@id",Db.P("@id",id));
}
public class BookingRepository {
 public DataTable All()=>Db.Query("SELECT b.booking_id AS ID,b.customer_id AS `Customer ID`,c.full_name AS Customer,b.car_id AS `Car ID`,CONCAT(a.make,' ',a.model) AS Car,b.start_date AS `Start date`,b.end_date AS `End date`,b.total_amount AS Total,b.status AS Status FROM bookings b JOIN customers c ON c.customer_id=b.customer_id JOIN cars a ON a.car_id=b.car_id ORDER BY b.booking_id DESC");
 public DataTable Customers()=>Db.Query("SELECT customer_id,full_name FROM customers ORDER BY full_name");
 public DataTable Cars()=>Db.Query("SELECT car_id,CONCAT(make,' ',model,' (',registration_number,')') AS car_name FROM cars WHERE status='Available'");
 public void Add(int c,int a,DateTime s,DateTime e,decimal t)=>Db.Execute("INSERT INTO bookings(customer_id,car_id,start_date,end_date,total_amount) VALUES(@c,@a,@s,@e,@t)",Db.P("@c",c),Db.P("@a",a),Db.P("@s",s.Date),Db.P("@e",e.Date),Db.P("@t",t));
 public void Status(int id,string s)=>Db.Execute("UPDATE bookings SET status=@s WHERE booking_id=@id",Db.P("@s",s),Db.P("@id",id));
 public void Delete(int id)=>Db.Execute("DELETE FROM bookings WHERE booking_id=@id",Db.P("@id",id));
}
public class PaymentRepository {
 public DataTable All()=>Db.Query("SELECT payment_id AS ID,booking_id AS `Booking ID`,amount AS Amount,payment_date AS Date,payment_method AS Method,payment_status AS Status FROM payments ORDER BY payment_id DESC");
 public DataTable Bookings()=>Db.Query("SELECT booking_id FROM bookings ORDER BY booking_id DESC");
 public void Add(int b,decimal a,DateTime d,string m,string s)=>Db.Execute("INSERT INTO payments(booking_id,amount,payment_date,payment_method,payment_status) VALUES(@b,@a,@d,@m,@s)",Db.P("@b",b),Db.P("@a",a),Db.P("@d",d.Date),Db.P("@m",m),Db.P("@s",s));
 public void Status(int id,string s)=>Db.Execute("UPDATE payments SET payment_status=@s WHERE payment_id=@id",Db.P("@s",s),Db.P("@id",id));
 public void Delete(int id)=>Db.Execute("DELETE FROM payments WHERE payment_id=@id",Db.P("@id",id));
}