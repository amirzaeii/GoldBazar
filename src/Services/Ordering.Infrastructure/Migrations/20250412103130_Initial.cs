﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateSequence(
                name: "buyerseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderitemseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymentseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "ordering",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "buyers",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IdentityGuid = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cardtypes",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cardtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethods",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CardTypeId = table.Column<int>(type: "integer", nullable: false),
                    BuyerId = table.Column<int>(type: "integer", nullable: false),
                    Alias = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CardHolderName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paymentmethods_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "ordering",
                        principalTable: "buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_paymentmethods_cardtypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalSchema: "ordering",
                        principalTable: "cardtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address_Street = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "text", nullable: false),
                    Address_State = table.Column<string>(type: "text", nullable: false),
                    Address_Country = table.Column<string>(type: "text", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "text", nullable: false),
                    BuyerId = table.Column<int>(type: "integer", nullable: true),
                    OrderStatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "ordering",
                        principalTable: "buyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orders_paymentmethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "ordering",
                        principalTable: "paymentmethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_buyers_IdentityGuid",
                schema: "ordering",
                table: "buyers",
                column: "IdentityGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderId",
                schema: "ordering",
                table: "orderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_BuyerId",
                schema: "ordering",
                table: "orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_PaymentMethodId",
                schema: "ordering",
                table: "orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_BuyerId",
                schema: "ordering",
                table: "paymentmethods",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_CardTypeId",
                schema: "ordering",
                table: "paymentmethods",
                column: "CardTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orderItems",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "paymentmethods",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "buyers",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "cardtypes",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "buyerseq",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "orderitemseq",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "paymentseq",
                schema: "ordering");
        }
    }
}
