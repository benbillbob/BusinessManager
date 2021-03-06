//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessManager.Model
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel;
    
    public partial class Roll : INotifyPropertyChanged
    {
    	public event PropertyChangedEventHandler PropertyChanged;
    
    	protected virtual void OnPropertyChanged(string propertyName)
    	{
            if (PropertyChanged!= null) 
    		{
    			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    		}
    	}
    
        public Roll()
        {
            this.StudentAttendences = new HashSet<StudentAttendence>();
        }
    
    	private System.Guid id;
    
        public System.Guid Id 
    	{ 
    		get { return id; } 
    		set
    		{
    			id = value;
    			OnPropertyChanged("Id");
    		} 
    	}
    
    	private Nullable<System.DateTime> date;
    
        public Nullable<System.DateTime> Date 
    	{ 
    		get { return date; } 
    		set
    		{
    			date = value;
    			OnPropertyChanged("Date");
    		} 
    	}
    
    	private Nullable<System.Guid> choirid;
    
        public Nullable<System.Guid> ChoirId 
    	{ 
    		get { return choirid; } 
    		set
    		{
    			choirid = value;
    			OnPropertyChanged("ChoirId");
    		} 
    	}
    
    
        public virtual Choir Choir { get; set; }
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
    }
}
