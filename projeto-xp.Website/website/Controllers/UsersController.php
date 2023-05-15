<?php
    require '../Models/UserModel.php';
    require '../Models/User.php';

    session_status() === PHP_SESSION_ACTIVE ? TRUE : session_start();

	class UsersController
	{

		function __construct()
		{
			$this->userModel =  new UserModel();
		}

		public function mvcHandler()
		{
			$act = isset($_GET['act']) ? $_GET['act'] : NULL;
			switch ($act)
			{
                case 'add' :
					$this->create();
					break;
				case 'update':
					$this->update();
					break;
				case 'delete':
					$this -> delete();
					break;
				default:
                    $this->list();
			}
		}
		public function pageRedirect($url)
		{
			header('Location:'.$url);
		}

		public function validateUser($user)
        {
            $validUser = true;

            if (empty($user->name))
            {
                $user->nameMsg = "Please insert a name.";
                $validUser = false;
            }
            if (empty($user->age))
            {
                $user->ageMsg = "Please insert an age.";
                $validUser = false;
            }
            else if($user->age < 0 || $user->age > 65535)
            {
                $user->ageMsg = "Age must be between 0 and 65535.";
                $validUser = false;
            }
            return $validUser;
        }

		public function create()
		{
            $user = new User();
            if (isset($_POST['createbutton']))
            {
                $user->name = trim($_POST['name']);
                $user->surname = trim($_POST['surname']);
                $user->age = trim($_POST['age']);

                $userValidation = $this->validateUser($user);
                if ($userValidation)
                {
                    $result = $this->userModel->postUser($user);
                    if (empty($result))
                    {
                        $_SESSION['usertable'] = serialize($user);
                        echo "Error: User could not be created.";
                        $this->pageRedirect("../View/create.php");
                    }
                    else
                    {
                        $this->list();
                    }
                }
                else
                {
                    $_SESSION['usertable'] = serialize($user);
                    $this->pageRedirect("../View/create.php");
                }
            }
        }

        public function update()
		{
            if (isset($_POST['updatebutton']))
            {
                $user = unserialize($_SESSION['usertable']);
                $user->id = trim($_POST['id']);
                $user->name = !empty(trim($_POST['name'])) ? trim($_POST['name']) : $user->name;
                $user->surname = trim($_POST['surname']);
                $user->age = trim($_POST['age']);

                $result = $this->userModel->putUser($user);
                if (empty($result))
                {
                    $_SESSION['usertable'] = serialize($user);
                    echo "Error: User could not be updated.";
                    $this->pageRedirect("../View/update.php");
                }
                else
                {
                    $this->list();
                }
            }
            else if (isset($_GET['id']) && !empty(trim($_GET["id"])))
            {
                $id = $_GET['id'];
                $result = $this->userModel->getUser($id);

                if (empty($result))
                {
                    echo "Error: User could not be retrieved.";
                }
                else
                {
                    $user = new User();
                    $user->id = $result->id;
                    $user->name= $result->name;
                    $user->surname = $result->surname;
                    $user->age = $result->age;
                }

                $_SESSION["usertable"] = serialize($user);
                $this->pageRedirect('../View/update.php');
            }
        }

        public function delete()
		{
            if (isset($_GET['id']))
            {
                $id=$_GET['id'];
                $res = $this->userModel->deleteUser($id);
                if($res = 204)
                {
                    $this->pageRedirect('../../public/index.php');
                }
                else
                {
                    echo "Error: Could not delete user";
                }
            }
            else
            {
                echo "Invalid operation.";
            }
        }

        public function list(){
            $result = $this->userModel->getAllUsers();
            include "../View/users.php";
        }
    }