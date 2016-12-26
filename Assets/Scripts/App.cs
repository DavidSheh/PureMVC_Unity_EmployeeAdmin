using UnityEngine;
using System.Collections;
using Demo.PureMVC.EmployeeAdmin;

public class App : MonoBehaviour {

    public MainWindow window;

    void Start ()
    {
        ApplicationFacade facade = (ApplicationFacade)ApplicationFacade.Instance;
        facade.Startup(window);
    }
}
