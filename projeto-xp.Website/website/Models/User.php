<?php

class User
{
    public $id;
    public $name;
    public $surname;
    public $age;
    public $creationDate;

    public $nameMsg;
    public $ageMsg;

    function __construct()
    {
        $id=$name=$surname=$age=$creationDate= "";
        $nameMsg=$ageMsg="";
    }
}