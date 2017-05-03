<?php 
	$con=mysqli_connect("localhost","root","","efollowme");
	// Check connection
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	
	mysqli_set_charset($con, "utf8");

	$username = $_POST['username'];
	$save = $_POST['save'];
	$score = $_POST['score'];


	

	$query = "UPDATE user SET user.save = '$save' WHERE user.username= '$username'";
	$result1 = $con->query($query);

	$count1=mysqli_affected_rows($con);

	$query = "UPDATE user SET user.score = '$score' WHERE user.username= '$username'";
	$result2 = $con->query($query);

	$count2=mysqli_affected_rows($con);
	





	if($count1==-1 || $count2==-1){
  
    echo "ERROR".  "</br>";
	}else if($count1==1 || $count2==1){
    	echo "OK".  "</br>";
    }else if ($count1==0 || $count2==0){
    	echo "NOTHING".  "</br>";
    }

?>