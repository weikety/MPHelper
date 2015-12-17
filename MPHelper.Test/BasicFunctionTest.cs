﻿using System.Linq;
using MPHelper.Dtos;
using NUnit.Framework;

namespace MPHelper.Test
{
	public class BasicFunctionTest
	{
		private MpManager _mpManager;

		[SetUp]
		protected void TestSetUp()
		{
			_mpManager = new MpManager(ConstData.MpAccount, ConstData.MpPasswordMd5).Preheat();
		}

		[Test]
		public void GetContactInfoTest()
		{
			var contactInfo = _mpManager.GetContactInfo(ConstData.FakeId);

			Assert.NotNull(contactInfo);
			Assert.AreEqual(ConstData.FakeId, contactInfo.fake_id);
		}

		[Test]
		public void GetSingleSendMessageListTest()
		{
			var messages = _mpManager.GetSingleSendMessageList(ConstData.FakeId);

			Assert.NotNull(messages);
			Assert.IsTrue(messages.Any());
		}

		[Test]
		public void GetAllMessageListTest()
		{
			var messages = _mpManager.GetAllMessageList(10);

			Assert.NotNull(messages);
			Assert.IsTrue(messages.Any());
		}

		[Test]
		public void GetMessageListByKeywordTest()
		{
			var messages = _mpManager.GetMessageListByKeyword("010227");

			Assert.NotNull(messages);
			Assert.IsTrue(messages.Any());
		}

		[Test]
		public void GetStarMessageListTest()
		{
			var messages = _mpManager.GetStarMessageList(1);

			Assert.NotNull(messages);
			Assert.IsTrue(messages.Any());
		}

		[Test]
		public async void SetStarMessageTest()
		{
			var messages = _mpManager.GetAllMessageList(1);

			Assert.NotNull(messages);

			var firstMessage = messages.First();

			Assert.NotNull(firstMessage);

			var success = await _mpManager.SetStarMessageAsync(firstMessage.id.ToString(), true);

			Assert.IsTrue(success);
		}

		[Test]
		public async void SingleSendMessageTest()
		{
			/*
			 * 可先给公众账号发送一条消息，确保突破48小时限制。
			 */

			const string message = "SingleSendMessageTest: test from MPHelper! 中文消息测试！";

			var success = await _mpManager.SingleSendMessageAsync(ConstData.FakeId, MpMessageType.Text, message);

			//var fileId = "10013378";
			//success = await _mpManager.SingleSendMessageAsync(ConstData.FakeId, MpMessageType.Image, fileId);

			//var appMsgId = "10013374";
			//success = await _mpManager.SingleSendMessageAsync(ConstData.FakeId, MpMessageType.AppMsg, appMsgId);

			Assert.IsTrue(success);
		}

		//[Test]
		//public async void MassSendMessageTest()
		//{
		//	/*
		//	 * 群发消息受公众账号限制（订阅号一天一条，服务号一个月一条），单元测试慎用。
		//	 */

		//	const string message = "MassSendMessageTest: test from MPHelper! 中文消息测试！";

		//	var success = await _mpManager.MassSendMessageAsync(MpMessageType.Text, message);

		//	Assert.IsTrue(success);
		//}

		[Test]
		public async void ChangeCategoryTest()
		{
			var success = await _mpManager.ChangeCategoryAsync(ConstData.FakeId, ConstData.CategoryId);

			Assert.IsTrue(success);
		}

		//[Test]
		//public void GetDonwloadFileBytesTest()
		//{
		//	var bytes = _mpManager.GetDonwloadFileBytes(200368584);

		//	Assert.IsTrue(bytes.Length > 0);
		//}
	}
}
