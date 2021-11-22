/*
 *  This is a block comment
 */
select /* all columns */ *
  from table1 /* We should select from table1
  And terminate the statement with semicolon
  */
; /*
    I should be part of the first statement!
*/

/*
 * I should be part of the second statement!
 */
select *
  from table2;
