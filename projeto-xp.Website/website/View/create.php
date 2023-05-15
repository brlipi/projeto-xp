<?php
        require '../Models/User.php';
        session_start();
        $userTable=isset($_SESSION['usertable'])?unserialize($_SESSION['usertable']):new User();
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Create Record</title>
    <link rel="stylesheet" href="../public/libs/bootstrap.css">
    <style type="text/css">
        .wrapper{
            width: 500px;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="page-header">
                        <h2>Create New User</h2>
                    </div>
                    <p>Name and Age fields are required.</p>
                    <form action="../public/index.php?act=add" method="post" >
                        <div class="form-group <?php echo (!empty($userTable->nameMsg)) ? 'has-error' : ''; ?>">
                            <label>Name</label>
                            <input type="text" name="name" class="form-control" value="<?php echo $userTable->name; ?>">
                            <span class="help-block"><?php echo $userTable->nameMsg;?></span>
                        </div>
                        <div class="form-group">
                            <label>Surname</label>
                            <input type="text" name="surname" class="form-control" value="<?php echo $userTable->surname; ?>">
                        </div>
                        <div class="form-group <?php echo (!empty($userTable->ageMsg)) ? 'has-error' : ''; ?>">
                            <label>Age</label>
                            <input type="text" name="age" class="form-control" value="<?php echo $userTable->age; ?>">
                            <span class="help-block"><?php echo $userTable->ageMsg;?></span>
                        </div>
                        <input type="submit" name="createbutton" class="btn btn-primary" value="Submit">
                        <a href="../public/index.php" class="btn btn-default">Cancel</a>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>