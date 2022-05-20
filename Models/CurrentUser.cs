using System;

namespace Autosalon.Models;

public static class CurrentUser
{
    private static Customer? instance;
    private static Manager? instance2;
    private static object syncRoot = new Object();

    public static Customer? getInstanceCustomer()
    {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new Customer();
            }
        }
        return instance;
    }
    public static void setInstanceCustomer(Customer? customer)
    {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = customer;
            }
        }
    }
    
    public static Manager? getInstanceManager()
    {
        if (instance2 == null)
        {
            lock (syncRoot)
            {
                if (instance2 == null)
                    instance = new Customer();
            }
        }
        return instance2;
    }
    public static void setInstanceManager(Manager? manager)
    {
        if (instance2 == null)
        {
            lock (syncRoot)
            {
                if (instance2 == null)
                    instance2 = manager;
            }
        }
    }
}