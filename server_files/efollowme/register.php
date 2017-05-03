<?php 
	$con=mysqli_connect("localhost","root","","efollowme");
	// Check connection
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	
	

	// clean user inputs to prevent sql injections
	$username = trim($_POST['username']);
	$username = strip_tags($username);
	$username = htmlspecialchars($username);


	$password = $_POST['password'];
	$password = strip_tags($password);
	$password = htmlspecialchars($password);



	$query = "SELECT username
			  FROM  user
			  WHERE user.username='$username'";


	$result = $con->query($query);

	
	$count = mysqli_num_rows($result);
   if($count!=0){
  
    echo "ERROR".  "</br>";
   }else{
   		$pass = hash('sha256', $password);
		$query = "INSERT INTO user(username,password) VALUES('$username','$pass')";
		$result = $con->query($query);
		if ($result) {
			echo "OK".  "</br>";

		}else{

			echo "ERROR".  "</br>";
		}
   }




?>