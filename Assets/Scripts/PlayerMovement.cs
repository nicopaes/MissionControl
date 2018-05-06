using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour 
{
	public float duration;
	public float progress;
	public AnimationCurve moveAnimCurve;

    private CreateMap _mapReference;
    private GameObject _newMapReference;

    private GameObject[,] _mapMatrixObj; 

	private Vector3[,] _mapMatrixVec;
	[SerializeField]
	private Vector2 _initialPos;	

	[SerializeField]
	private Vector2 _playerPositionVec2;
	private Vector2 _playerLastDirection;
	private Vector3 _playerLastPosition;
	private Vector3 _playerTarget;

    private float _currentLerpTime;
    private float _perc;
	[SerializeField]
	private bool _moving;


    void OnEnable()
    {
        Camera.main.GetComponent<CameraFollow>().SetTarget(this.transform);


        _mapReference = GameObject.Find("MAP").GetComponent<CreateMap>();
        _mapMatrixVec = _mapReference.MapMatrixVec;

        _newMapReference = GameObject.Find("MAP");

        _playerTarget = transform.position;
    }
	void Update()
	{
		if(transform.position != _playerTarget)
		{
			_moving = true;
			if(transform.position != _playerTarget)
			{      
				//increment timer once per frame
					_currentLerpTime += Time.deltaTime;
				if (_currentLerpTime > duration) 
				{
					_currentLerpTime = duration;
				}
		
				//lerp!
				_perc = _currentLerpTime / duration;
				_perc = moveAnimCurve.Evaluate(_perc);
				transform.position = Vector3.LerpUnclamped(_playerLastPosition, _playerTarget, _perc);
			}
		}
		else
		{
			_moving = false;
		}
        if (_perc >= 0.6f)
        {
            //_mapReference.DeactivateTile(_playerPositionVec2 - _playerLastDirection);
            //if (_mapReference.ActivateTile(_playerPositionVec2))
            //{
            //    GetComponent<PlayerHealth>().TakeDamage(10);
            //};
        }
        
        for (int i = 0; i < _newMapReference.transform.GetChild(0).childCount; i++)
        {
            if (_newMapReference.transform.GetChild(0).GetChild(i).position == transform.position)
            {
                _newMapReference.transform.GetChild(0).GetChild(i).GetComponent<TileComponent>().active = true;
                if(_newMapReference.transform.GetChild(0).GetChild(i).GetComponent<TileComponent>().active && _newMapReference.transform.GetChild(0).GetChild(i).GetComponent<TileComponent>().danger)
                {
                    GetComponent<PlayerHealth>().TakeDamage(10);
                }
            }
            else
            {
                _newMapReference.transform.GetChild(0).GetChild(i).GetComponent<TileComponent>().active = false;
            }
            
        }

        if (Input.GetKeyDown("up"))
		{
			Move(new Vector2(0,-1));
		}
		if(Input.GetKeyDown("down"))
		{
			Move(new Vector2(0,1));
		}
		if(Input.GetKeyDown("left"))
		{
			Move(new Vector2(-1,0));
		}
		if(Input.GetKeyDown("right"))
		{
			Move(new Vector2(1,0));
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//Cancel();
		}
	}
	public void Move(Vector2 direction)
	{
		if(!_moving)
		{
            for (int i = 0; i < _newMapReference.transform.GetChild(0).childCount; i++)
            {
                if ((Vector2)_newMapReference.transform.GetChild(0).GetChild(i).position == new Vector2(transform.position.x + direction.x, transform.position.y - direction.y))
                {
                    Debug.Log(_newMapReference.transform.GetChild(0).GetChild(i).name);
                    if(_newMapReference.transform.GetChild(0).GetChild(i).GetComponent<TileComponent>().path)
                    {
                        _currentLerpTime = 0f;
                        _playerLastPosition = transform.position;

                        _playerLastDirection = direction;
                        _playerPositionVec2 += direction;

                        //_playerTarget = new Vector2(_playerPositionVec2.x, -_playerPositionVec2.y);
                        _playerTarget = new Vector2(transform.position.x + direction.x, transform.position.y - direction.y);
                    }
                    else
                    {
                        Terminal.WriteLine("Tile Blocked");
                    }
                }

            }
            /*
            if ((_mapReference.CheckNextPos(new Vector2(_playerPositionVec2.x + direction.x, _playerPositionVec2.y + direction.y)) != new Vector3(1,1,1)))
            {
                _currentLerpTime = 0f;
                _playerLastPosition = transform.position;

                _playerLastDirection = direction;
                _playerPositionVec2 += direction;

                _playerTarget = new Vector2(_playerPositionVec2.x,-_playerPositionVec2.y);
                //_playerTarget = _mapReference.CheckNextPos(_playerPositionVec2);
            }*/
        }
	}
	public void Cancel()
	{
		if(_perc <= 0.6f)
		{
			_currentLerpTime = 0f;
			_playerTarget = _playerLastPosition;
			_playerLastPosition = transform.position;
			_playerPositionVec2 -= _playerLastDirection;
		}
	}
	public void SetInitialPos(Vector3 initPos)
	{
		_initialPos = initPos;
		_playerPositionVec2 = _initialPos;
	}
    void OnUserInput(string input)
    {
        Debug.Log("Input");
        switch (input.ToLower())
        {
            case "go east":
                Move(new Vector2(1,0));
                break;
            case "go west":
                Move(new Vector2(-1,0));
                break;
            case "go south":
                Move(new Vector2(0,1));
                break;
            case "go north":
                Move(new Vector2(0,-1));
                break;
            case "cancel":
                Cancel();
                break;

        }
        //Terminal.WriteLine("The user typed " + input);
    }
}
