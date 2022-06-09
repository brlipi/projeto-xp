<?php

session_unset();
require_once  '../Controllers/UsersController.php';		
$controller = new UsersController();
$controller->mvcHandler();
?>