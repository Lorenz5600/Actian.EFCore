$user = $args[0]
$database = $args[1]

createdb $user -n $database

if ( $LASTEXITCODE ) {
  exit $LASTEXITCODE
}

if ( $? -eq 0 ) {
  exit $?
}
