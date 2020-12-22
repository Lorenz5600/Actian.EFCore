# $OutputEncoding = [Text.UTF8Encoding]::new($false)

$user = $args[0]

$sql = @"
commit
\continue\p\g

drop user "$user"
\continue\p\g

commit
\continue\p\g

create user "$user"
  with privileges = (createdb, trace, security, operator, maintain_users)
\continue\p\g

commit
\continue\p\g

alter user "$user"
  with privileges = (createdb, trace, security, operator, maintain_users)
\nocontinue\p\g

commit
\nocontinue\p\g
"@


$sql | sql iidbdb

if ( $LASTEXITCODE ) {
  exit $LASTEXITCODE
}

if ( $? -eq 0 ) {
  exit $?
}
