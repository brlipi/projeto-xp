<?php
	
	class UserModel
	{
        private $url;
        private $headers = array();

		function __construct()
		{
            $this->url = "http://api:80/Users";
            $this->headers = array(
                "User-Agent: PHP Website",
                "Content-Type: application/json",
                "Accept: */*",
                "Connection: keep-alive",
            );
		}
        
        public function getAllUsers()
		{
            $curlSession = curl_init($this->url);

            $options = array(
                CURLOPT_HTTPHEADER => $this->headers,
                CURLOPT_RETURNTRANSFER => TRUE
            );

            curl_setopt_array($curlSession, $options);

            // See how to get response code and do an if else maybe?
            $response = curl_exec($curlSession);

            $content = json_decode($response, true);

            curl_close($curlSession);

            return $content;
		}

        public function getUser($id)
        {
            $curlSession = curl_init($this->url . "/" . $id);

            $options = array(
                CURLOPT_HTTPHEADER => $this->headers,
                CURLOPT_RETURNTRANSFER => TRUE
            );

            curl_setopt_array($curlSession, $options);

            // See how to get response code and do an if else maybe?
            $response = curl_exec($curlSession);

            $content = json_decode($response);

            curl_close($curlSession);

            return $content;
        }

		public function postUser($user)
		{
            $data = [
                'name' => $user->name,
                'surname' => $user->surname,
                'age' => (int) $user->age
            ];

            $post = json_encode($data);
            $curlSession = curl_init($this->url);

                $options = array(
                    CURLOPT_HTTPHEADER => $this->headers,
                    CURLOPT_POSTFIELDS => $post,
                    CURLOPT_RETURNTRANSFER => TRUE
                );

                curl_setopt_array($curlSession, $options);
                
                // See how to get response code and do an if else maybe?
                $content = curl_exec($curlSession);

                curl_close($curlSession);

                return $content;
		}

		public function putUser($user)
		{
            $data = [
                'name' => $user->name,
                'surname' => $user->surname,
                'age' => $user->age,
            ];
            $id = $user->id;

            $put = json_encode($data);
            $curlSession = curl_init($this->url . "/" . $id);

            $options = array(
                CURLOPT_HTTPHEADER => $this->headers,
                CURLOPT_CUSTOMREQUEST => 'PUT',
                CURLOPT_POSTFIELDS => $put,
                CURLOPT_RETURNTRANSFER => TRUE
            );

            curl_setopt_array($curlSession, $options);

            // See how to get response code and do an if else maybe?
            $content = curl_exec($curlSession);

            curl_close($curlSession);

            return $content;
        }

		public function deleteUser($id)
		{	
            $curlSession = curl_init($this->url . "/" . $id);

            $options = array(
                CURLOPT_HTTPHEADER => $this->headers,
                CURLOPT_CUSTOMREQUEST => "DELETE",
                CURLOPT_RETURNTRANSFER => TRUE
            );

            curl_setopt_array($curlSession, $options);
            $response = curl_exec($curlSession);
            $responseCode = curl_getinfo($curlSession, CURLINFO_HTTP_CODE);

            curl_close($curlSession);

            return $responseCode;
        }
	}