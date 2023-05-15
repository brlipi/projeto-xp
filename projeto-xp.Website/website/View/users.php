<?php session_unset();?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="UTF-8"/>
        <title>projeto-xp</title>
        <link rel="stylesheet" href="libs/bootstrap.css">
        <script src="libs/jquery.min.js"></script>
        <script src="libs/bootstrap.js"></script>
        <style type="text/css">
            .wrapper{
                width: 60%;
                margin: 0 auto;
            }
            .page-header h2{
                margin-top: 0;
                padding-left: 27%;
            }
            .buttons a{
                margin-left: 10px;
            }
        </style>
        <script type="text/javascript">
        $(document).ready(function(){
            $('[data-toggle="tooltip"]').tooltip();
        });
        </script>
    </head>
    <body>
        <div class="wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-auto">
                        <div class="page-header clearfix">
                            <a href="index.php" class="btn btn-success pull-left">Home</a>
                            <h2 class="pull-left">Registered Users</h2>
                            <a href="../View/create.php" class="btn btn-success pull-right">Create New User</a>
                        </div>
                        <?php
                        if(count($result) > 0){
                            $i = 0;
                            echo "<table class='table table-bordered table-striped'>";
                                echo "<thead>";
                                    echo "<tr>";
                                        echo "<th>Id</th>";
                                        echo "<th>Name</th>";
                                        echo "<th>Surname</th>";
                                        echo "<th>Age</th>";
                                        echo "<th>Creation Date</th>";
                                        echo "<th>Action</th>";
                                    echo "</tr>";
                                echo "</thead>";
                                echo "<tbody>";
                                while($i < count($result)){
                                    echo "<tr>";
                                        echo "<td>" . $result[$i]["id"] . "</td>";
                                        echo "<td>" . $result[$i]['name'] . "</td>";
                                        echo "<td>" . $result[$i]['surname'] . "</td>";
                                        echo "<td>" . $result[$i]['age'] . "</td>";
                                        echo "<td>" . $result[$i]['creationDate'] . "</td>";
                                        echo "<td class = 'buttons'>";
                                        echo "<a href='index.php?act=update&id=". $result[$i]['id'] ."' title='Update Record' data-toggle='tooltip' class='btn btn-warning'>Update User</a>";
                                        echo "<a href='index.php?act=delete&id=". $result[$i]['id'] ."' title='Delete Record' data-toggle='tooltip' class='btn btn-danger'>Delete User</a>";
                                        echo "</td>";
                                    echo "</tr>";
                                    $i++;
                                }
                                echo "</tbody>";
                            echo "</table>";
                        } else{
                            echo "<center><p class='lead'><em>No registered users yet. Use the \"Create User\" button to register a new one.</em></p></center>";
                        }
                    ?>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>