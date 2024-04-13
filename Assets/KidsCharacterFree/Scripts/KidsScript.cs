using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample {
public class KidsScript : MonoBehaviour
{
  private Animator _Animator;
  private CharacterController _Ctrl;
  private Vector3 _MoveDirection = Vector3.zero;
  private GameObject _View_Camera;
  private Transform _Light;
  private SkinnedMeshRenderer _MeshRenderer;
  // Hash
  private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
  private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
  private static readonly int JumpState = Animator.StringToHash("Base Layer.jump");
  private static readonly int DamageState = Animator.StringToHash("Base Layer.damage");
  private static readonly int DownState = Animator.StringToHash("Base Layer.down");
  private static readonly int FaintState = Animator.StringToHash("Base Layer.faint");
  private static readonly int StandUpFaintState = Animator.StringToHash("Base Layer.standup_faint");

  private static readonly int JumpTag = Animator.StringToHash("Jump");
  private static readonly int DamageTag = Animator.StringToHash("Damage");
  private static readonly int FaintTag = Animator.StringToHash("Faint");

  private static readonly int SpeedParameter = Animator.StringToHash("Speed");
  private static readonly int JumpPoseParameter = Animator.StringToHash("JumpPose");

  void Start()
  {
    _Animator = this.GetComponent<Animator>();
    _Ctrl = this.GetComponent<CharacterController>();
    _View_Camera = GameObject.Find("Main Camera");
    _Light = GameObject.Find("Directional Light").transform;
    _MeshRenderer = this.transform.Find("Boy0.Humanoid.Body").gameObject.GetComponent<SkinnedMeshRenderer>();
  }

  void Update()
  {
    CAMERA();
    DIRECTION_LIGHT();
    GRAVITY();
    STATUS();

    if(!_Status.ContainsValue( true ))
    {
        MOVE();
        JUMP();
        DAMAGE();
        FAINT();
    }
    else if(_Status.ContainsValue( true ))
    {
      int status_name = 0;
      foreach(var i in _Status)
      {
        if(i.Value == true)
        {
          status_name = i.Key;
          break;
        }
      }
      if(status_name == Jump)
      {
        MOVE();
        JUMP();
        FAINT();
      }
      else if(status_name == Damage)
      {
        DAMAGE();
      }
      else if(status_name == Faint)
      {
        FAINT();
      }
    }
  }
  //--------------------------------------------------------------------- STATUS
  // Flags to control slime's action
  // It is used by method in Update()
  //---------------------------------------------------------------------
  private const int Jump = 1;
  private const int Damage = 2;
  private const int Faint = 3;
  private Dictionary<int, bool> _Status = new Dictionary<int, bool>
  {
      {Jump, false },
      {Damage, false },
      {Faint, false },
  };
  //------------------------------
  private void STATUS ()
  {
      if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash == JumpTag)
      {
          _Status[Jump] = true;
      }
      else if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != JumpTag)
      {
          _Status[Jump] = false;
      }

      if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash == DamageTag)
      {
          _Status[Damage] = true;
      }
      else if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != DamageTag)
      {
          _Status[Damage] = false;
      }

      if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash == FaintTag)
      {
          _Status[Faint] = true;
      }
      else if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != FaintTag)
      {
          _Status[Faint] = false;
      }
  }
  //--------------------------------------------------------------------- CAMERA
  // camera moving
  //---------------------------------------------------------------------
  private void CAMERA ()
  {
    _View_Camera.transform.position = this.transform.position + new Vector3(0, 0.5f, 2.0f);
  }
  //--------------------------------------------------------------------- DIRECTION_LIGHT
  // Direction of light
  //---------------------------------------------------------------------
  private void DIRECTION_LIGHT ()
  {
    Vector3 pos = _Light.position - this.transform.position;
    _MeshRenderer.material.SetVector("_LightDir", pos);
  }
  //--------------------------------------------------------------------- GRAVITY
  // gravity for fall of slime
  //---------------------------------------------------------------------
  private void GRAVITY ()
  {
    if(CheckGrounded())
    {
      if(_MoveDirection.y < -0.1f){
        _MoveDirection.y = -0.1f;
      }
    }
    _MoveDirection.y -= 0.1f;
    _Ctrl.Move(_MoveDirection * Time.deltaTime);
  }
  //--------------------------------------------------------------------- isGrounded
  // whether it is grounded
  //---------------------------------------------------------------------
  private bool CheckGrounded()
  {
    if (_Ctrl.isGrounded){
      return true;
    }
    Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
    float range = 0.11f;
    return Physics.Raycast(ray, range);
  }
  //--------------------------------------------------------------------- MOVE
  // for slime moving
  //---------------------------------------------------------------------
  private void MOVE ()
  {
    float speed = _Animator.GetFloat(SpeedParameter);
    //------------------------------------------------------------ Speed
    if(Input.GetKey(KeyCode.Z))
    {
      if(speed <= 2){
        speed += 0.01f;
      }
      else if(speed >= 2){
        speed = 2;
      }
    }
    else {
      if(speed >= 1){
        speed -= 0.01f;
      }
      else if(speed <= 1){
        speed = 1;
      }
    }
    _Animator.SetFloat(SpeedParameter, speed);

    //------------------------------------------------------------ Forward
    if (Input.GetKey(KeyCode.UpArrow))
    {
      // velocity
      if(_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
      {
        Vector3 velocity = this.transform.rotation * new Vector3(0, 0, speed);
        MOVE_XZ(velocity);
        MOVE_RESET();
      }
    }
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != JumpTag){
        _Animator.CrossFade(MoveState, 0.1f, 0, 0);
      }
    }

    //------------------------------------------------------------ character rotation
    if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)){
      this.transform.Rotate(Vector3.up, 1.0f);
    }
    else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
      this.transform.Rotate(Vector3.up, -1.0f);
    }
    if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
    {
      if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != JumpTag)
      {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)){
          _Animator.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
          _Animator.CrossFade(MoveState, 0.1f, 0, 0);
        }
      }
      // rotate stop
      else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
      {
        if(_Animator.GetCurrentAnimatorStateInfo(0).tagHash != JumpTag){
          _Animator.CrossFade(IdleState, 0.1f, 0, 0);
        }
      }
    }
    KEY_UP();
  }
  //--------------------------------------------------------------------- KEY_UP
  // whether arrow key is key up
  //---------------------------------------------------------------------
  private void KEY_UP ()
  {
    if(_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != JumpState
        && !_Animator.IsInTransition(0))
    {
      if (Input.GetKeyUp(KeyCode.UpArrow))
      {
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
          _Animator.CrossFade(IdleState, 0.1f, 0, 0);
        }
      }
      else if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
      {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
          if(Input.GetKey(KeyCode.LeftArrow)){
            _Animator.CrossFade(MoveState, 0.1f, 0, 0);
          }
          else if(Input.GetKey(KeyCode.RightArrow)){
            _Animator.CrossFade(MoveState, 0.1f, 0, 0);
          }
          else{
            _Animator.CrossFade(IdleState, 0.1f, 0, 0);
          }
        }
      }
    }
  }
  //--------------------------------------------------------------------- MOVE_SUB
  // value for moving
  //---------------------------------------------------------------------
  private void MOVE_XZ (Vector3 velocity)
  {
      _MoveDirection = new Vector3 (velocity.x, _MoveDirection.y, velocity.z);
      _Ctrl.Move(_MoveDirection * Time.deltaTime);
  }
  private void MOVE_RESET()
  {
      _MoveDirection.x = 0;
      _MoveDirection.z = 0;
  }
  //--------------------------------------------------------------------- JUMP
  // for jumping
  //---------------------------------------------------------------------
  private void JUMP ()
  {
    if(CheckGrounded())
    {
      if(Input.GetKeyDown(KeyCode.S)
          && _Animator.GetCurrentAnimatorStateInfo(0).tagHash != JumpTag
          && !_Animator.IsInTransition(0))
      {
        _Animator.CrossFade(JumpState, 0.1f, 0, 0);
        // jump power
        _MoveDirection.y = 8.0f;
        _Animator.SetFloat(JumpPoseParameter, _MoveDirection.y);
      }
      if (_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == JumpState
          && !_Animator.IsInTransition(0)
          && JumpPoseParameter < 0)
      {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
          _Animator.CrossFade(MoveState, 0.3f, 0, 0);
        }
        else{
          _Animator.CrossFade(IdleState, 0.3f, 0, 0);
        }
      }
    }
    else if(!CheckGrounded())
    {
      if (_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == JumpState
          && !_Animator.IsInTransition(0))
        {
          _Animator.SetFloat(JumpPoseParameter, _MoveDirection.y);
        }
      if(_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != JumpState
            && !_Animator.IsInTransition(0))
        {
          _Animator.CrossFade(JumpState, 0.1f, 0, 0);
        }
    }
  }
  //--------------------------------------------------------------------- DAMAGE
  // play animation of damage
  //---------------------------------------------------------------------
  private void DAMAGE ()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      _Animator.CrossFade(DamageState, 0.1f, 0, 0);
    }
    if (_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
        && _Animator.GetCurrentAnimatorStateInfo(0).tagHash == DamageTag
        && !_Animator.IsInTransition(0))
    {
      _Animator.CrossFade(IdleState, 0.3f, 0, 0);
    }
  }
  //--------------------------------------------------------------------- FAINT
  // play animation of down and jump of resurrection
  //---------------------------------------------------------------------
  private void FAINT ()
  {
    if (Input.GetKeyDown(KeyCode.W))
    {
      _Animator.CrossFade(DownState, 0.1f, 0, 0);
    }
    if (_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
        && _Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == DownState
        && !_Animator.IsInTransition(0))
    {
      _Animator.CrossFade(FaintState, 0.3f, 0, 0);
    }

    if (Input.GetKeyDown(KeyCode.E)
        && _Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == FaintState
        && !_Animator.IsInTransition(0))
    {
      _Animator.CrossFade(StandUpFaintState, 0.1f, 0, 0);
    }
  }
}
}
