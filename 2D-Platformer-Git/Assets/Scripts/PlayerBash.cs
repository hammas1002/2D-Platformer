using UnityEngine;

public class PlayerBash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]

    float dir;
    [SerializeField]

    [Header("Bash")]
    public float bashObjDetectRadius = 1f;
    bool isBashing = false;
    public bool IsBashing { get => isBashing; }
    Vector2 bashDir = Vector2.zero;
    public float bashPower = 50f;
    public float bashTime = 0.7f;
    float bashTimeReset;
    
    bool bashableObjInRange = false;
    public GameObject bashableObj;
    public GameObject arrow;
    bool isChoosingDir = false;
    public LayerMask bashObjLayerMask;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        bashTimeReset = bashTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Bash()
    {
       
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, bashObjDetectRadius, Vector3.forward, 0f, bashObjLayerMask);
        if (hit2D)
        {
            print("Object in range");
            bashableObjInRange = true;
            bashableObj = hit2D.collider.transform.gameObject;
        }
        else
        {
            bashableObjInRange = false;
        }
            
        if (bashableObjInRange)
        {
            print("Changing color");
            bashableObj.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Time.timeScale = 0f;
                bashableObj.transform.localScale = new Vector2(1.4f,1.4f);
                isChoosingDir = true;
                arrow.transform.position = bashableObj.transform.position;
                arrow.SetActive(true);
                return;
            }
            else if(isChoosingDir&& Input.GetKeyUp(KeyCode.Mouse1))
            {
                Time.timeScale = 1;
                bashableObj.transform.localScale = new Vector2(1f, 1f);
                arrow.SetActive(false);
                isChoosingDir = false;
                isBashing = true;
                rb.velocity = Vector2.zero;
                transform.position = bashableObj.transform.position;
                bashDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                bashDir = bashDir.normalized;
                print("Bash Dir:"+bashDir);
                bashableObj?.GetComponent<Rigidbody2D>().AddForce(-bashDir*30,ForceMode2D.Impulse);

            }
        }
        else if(bashableObj!=null)
        {   
            bashableObj.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        if (isBashing)
        {
            if (bashTime>0)
            {
                bashTime -= Time.fixedDeltaTime;

                rb.velocity = bashDir * bashPower * Time.fixedDeltaTime;

            }
            else
            {
                isBashing = false;
                bashTime = bashTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, bashObjDetectRadius);
        
    
    }
}
