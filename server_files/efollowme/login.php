<?php 
	$con=mysqli_connect("localhost","root","","efollowme");
	// Check connection
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	mysqli_set_charset($con, "utf8");


	// clean user inputs to prevent sql injections
	$username = trim($_POST['username']);
	$username = strip_tags($username);
	$username = htmlspecialchars($username);


	$password = $_POST['password'];
	$password = strip_tags($password);
	$password = htmlspecialchars($password);



	$pass = hash('sha256', $password);


	$query = "SELECT username,password,save
			  FROM  user
			  WHERE user.username='$username'AND user.password='$pass'";


	$result = $con->query($query);

	
	while ($row = $result->fetch_array(MYSQLI_NUM)) {
		$count = 3; //fields
		for($i=0; $i<$count; $i++){
			if($i!=($count-1)){
				echo $row[$i] . '@';
			}
			else{
				echo $row[$i].  "</br>";
			}
			
			
		}
	}
?>