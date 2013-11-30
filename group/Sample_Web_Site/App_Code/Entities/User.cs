using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    //*************************************************************************
    // Fields
    //*************************************************************************
    private UInt64 id;
    private String fname;
    private String lname;
    private String email;
    private String password;

    //*************************************************************************
    // Constructor
    //*************************************************************************
	public User()
	{
        id = 1;
        fname = "";
        lname = "";
        email = "";
        password = "";
	}

    public User(UInt64 id, String fname, String lname, String email, String password)
    {
        this.id = id;
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.password = password;
    }

    //*************************************************************************
    // Methods
    //*************************************************************************
    public UInt64 ID { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
}